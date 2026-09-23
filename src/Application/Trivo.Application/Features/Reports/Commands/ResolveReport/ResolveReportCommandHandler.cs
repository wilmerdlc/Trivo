using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Email;
using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Commands.ResolveReport;

internal sealed class ResolveReportCommandHandler(
    IReportRepository reportRepository,
    ISanctionRepository sanctionRepository,
    IUserRepository userRepository,
    INotificationService notificationService,
    IEmailService emailService,
    ICacheService cache,
    IUnitOfWork unitOfWork,
    ILogger<ResolveReportCommandHandler> logger
) : ICommandHandler<ResolveReportCommand, ReportResolutionDto>
{
    public async Task<ResultT<ReportResolutionDto>> Handle(
        ResolveReportCommand request,
        CancellationToken cancellationToken)
    {
        var report = await reportRepository.GetByIdAsync(request.ReportId, cancellationToken);
        if (report is null)
        {
            logger.LogWarning("No report was found with ID '{ReportId}'.", request.ReportId);

            return ResultT<ReportResolutionDto>.Failure(Error.NotFound("404", "Report not found."));
        }

        if (report.ReportStatus != ReportStatus.Pending.ToString())
        {
            logger.LogWarning("Report '{ReportId}' was already resolved ({Status}).", report.ReportId, report.ReportStatus);

            return ResultT<ReportResolutionDto>.Failure(
                Error.Conflict("409", "This report has already been resolved.")
            );
        }

        var decision = Enum.Parse<ReportDecision>(request.Decision, ignoreCase: true);
        var now = DateTime.UtcNow;

        report.FinalReason = request.FinalReason;
        report.ReviewedAt = now;
        report.ReviewedByAdminId = request.AdminId;

        if (decision == ReportDecision.Rejected)
        {
            return await RejectAsync(report, request, now, cancellationToken);
        }

        return await ApproveAsync(report, request, now, cancellationToken);
    }

    #region Private Methods
    private async Task<ResultT<ReportResolutionDto>> RejectAsync(
        Report report,
        ResolveReportCommand request,
        DateTime now,
        CancellationToken cancellationToken)
    {
        report.ReportStatus = ReportStatus.Rejected.ToString();

        await reportRepository.UpdateAsync(report, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Report '{ReportId}' rejected by administrator '{AdminId}'.", report.ReportId, request.AdminId);

        await NotifyAsync(
            report.ReportedById!.Value,
            $"Your report was reviewed and did not proceed. Reason: {request.FinalReason}",
            cancellationToken);

        return ResultT<ReportResolutionDto>.Success(ToResolution(report, null, now));
    }

    private async Task<ResultT<ReportResolutionDto>> ApproveAsync(
        Report report,
        ResolveReportCommand request,
        DateTime now,
        CancellationToken cancellationToken)
    {
        var reportedUser = await userRepository.GetByIdAsync(report.ReportedUserId!.Value, cancellationToken);
        if (reportedUser is null)
        {
            logger.LogWarning("Reported user '{UserId}' of report '{ReportId}' no longer exists.", report.ReportedUserId, report.ReportId);

            return ResultT<ReportResolutionDto>.Failure(Error.NotFound("404", "The reported user was not found."));
        }

        var type = Enum.Parse<SanctionType>(request.SanctionType!, ignoreCase: true);

        var sanction = new Sanction
        {
            Id = Guid.NewGuid(),
            UserId = reportedUser.Id,
            ReportId = report.ReportId,
            AdminId = request.AdminId,
            Type = type.ToString(),
            Reason = request.FinalReason,
            CreatedAt = now,
            ExpiresAt = type == SanctionType.TemporarySuspension ? now.AddDays(request.DurationDays!.Value) : null
        };

        report.ReportStatus = ReportStatus.Approved.ToString();

        // The account status only ever escalates: a suspension must never lift an existing ban.
        var newStatus = type switch
        {
            SanctionType.PermanentBan => UserStatus.Banned,
            SanctionType.TemporarySuspension when reportedUser.UserStatus != nameof(UserStatus.Banned) => UserStatus.Suspended,
            _ => (UserStatus?)null
        };

        await sanctionRepository.CreateAsync(sanction, cancellationToken);
        await reportRepository.UpdateAsync(report, cancellationToken);

        if (newStatus is not null)
        {
            reportedUser.UserStatus = newStatus.ToString();
            reportedUser.UpdatedAt = now;
            await userRepository.UpdateAsync(reportedUser, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.InvalidateByTagsAsync([CacheKeys.AdminUsersTag, CacheKeys.UserTag(reportedUser.Id)], cancellationToken);

        logger.LogInformation(
            "Report '{ReportId}' approved by administrator '{AdminId}'. Sanction '{SanctionType}' applied to user '{UserId}'.",
            report.ReportId, request.AdminId, type, reportedUser.Id);

        await NotifyAsync(reportedUser.Id, BuildSanctionMessage(sanction, type), cancellationToken);

        if (request.NotifyByEmail && !string.IsNullOrWhiteSpace(reportedUser.Email))
        {
            await SendSanctionEmailAsync(reportedUser, sanction, type);
        }

        return ResultT<ReportResolutionDto>.Success(ToResolution(report, sanction, now));
    }

    private static ReportResolutionDto ToResolution(Report report, Sanction? sanction, DateTime now) =>
        new(
            ReportId: report.ReportId ?? Guid.Empty,
            ReportStatus: report.ReportStatus!,
            FinalReason: report.FinalReason!,
            ReviewedAt: report.ReviewedAt ?? now,
            Sanction: sanction?.ToSanctionDto(now)
        );

    private static string BuildSanctionMessage(Sanction sanction, SanctionType type) => type switch
    {
        SanctionType.Warning =>
            $"You have received a warning for breaking the platform's policies. Reason: {sanction.Reason}",
        SanctionType.TemporarySuspension =>
            $"Your account has been suspended until {sanction.ExpiresAt:yyyy-MM-dd HH:mm} UTC. Reason: {sanction.Reason}",
        _ =>
            $"Your account has been permanently banned. Reason: {sanction.Reason}"
    };

    // The decision is already committed at this point, so a failure to notify must not turn a
    // successful resolution into an error response — log it and move on.
    private async Task NotifyAsync(Guid userId, string content, CancellationToken cancellationToken)
    {
        try
        {
            var result = await notificationService.CreateNotificationByTypeAsync(
                userId, NotificationType.Alert.ToString(), content, cancellationToken);

            if (!result.IsSuccess)
            {
                logger.LogWarning("Could not create the in-app notification for user '{UserId}': {Error}", userId, result.Error!.Description);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Could not create the in-app notification for user '{UserId}'.", userId);
        }
    }

    private async Task SendSanctionEmailAsync(User user, Sanction sanction, SanctionType type)
    {
        try
        {
            await emailService.SendEmailAsync(
                new EmailResponseDto(
                    User: user.Email!,
                    Body: EmailTemplate.AccountSanction(user.Username ?? user.FirstName ?? string.Empty, type.ToString(), sanction.Reason!, sanction.ExpiresAt),
                    Subject: "Action taken on your Trivo account"
                )
            );
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Could not send the sanction email to user '{UserId}'.", user.Id);
        }
    }
    #endregion
}
