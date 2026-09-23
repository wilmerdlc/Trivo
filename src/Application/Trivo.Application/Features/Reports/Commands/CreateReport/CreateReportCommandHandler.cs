using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports.Commands.CreateReport;

internal sealed class CreateReportCommandHandler(
    IReportRepository reportRepository,
    IMessageRepository messageRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateReportCommandHandler> logger
) : ICommandHandler<CreateReportCommand, ReportDto>
{
    internal static ReportType? ResolveType(CreateReportCommand command)
    {
        if (command.ReportType is null)
        {
            return command.MessageId is not null ? ReportType.Message : null;
        }

        return Enum.TryParse<ReportType>(command.ReportType, ignoreCase: true, out var type) ? type : null;
    }

    public async Task<ResultT<ReportDto>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var reportingUser = await userRepository.GetByIdAsync(request.ReportedById, cancellationToken);
        if (reportingUser is null)
        {
            logger.LogError("The user with id {UserId} does not exist.", request.ReportedById);

            return ResultT<ReportDto>.Failure(Error.NotFound("404", "The reporting user was not found."));
        }

        var type = ResolveType(request)!.Value;

        Message? message = null;
        User? reportedUser;
        string? content;
        string? contentType = null;

        if (type == ReportType.Message)
        {
            message = await messageRepository.GetWithUsersByIdAsync(request.MessageId!.Value, cancellationToken);
            if (message is null)
            {
                logger.LogError("The message with id {MessageId} does not exist.", request.MessageId);

                return ResultT<ReportDto>.Failure(Error.NotFound("404", "The message to report was not found."));
            }

            if (message.SenderId == reportingUser.Id)
            {
                logger.LogWarning("User {UserId} tried to report their own message {MessageId}.", reportingUser.Id, message.MessageId);

                return ResultT<ReportDto>.Failure(Error.Validation("400", "You can't report your own message."));
            }

            if (message.ReceiverId != reportingUser.Id)
            {
                logger.LogWarning("User {UserId} tried to report message {MessageId}, which they didn't receive.", reportingUser.Id, message.MessageId);

                return ResultT<ReportDto>.Failure(Error.Forbidden("403", "You can only report messages you received."));
            }

            reportedUser = message.Sender;
            content = message.Content;
            contentType = message.Type;
        }
        else
        {
            if (request.ReportedUserId == reportingUser.Id)
            {
                return ResultT<ReportDto>.Failure(Error.Validation("400", "You can't report yourself."));
            }

            reportedUser = await userRepository.GetByIdAsync(request.ReportedUserId!.Value, cancellationToken);
            content = reportedUser?.ToProfileSnapshot();
        }

        if (reportedUser is null)
        {
            logger.LogError("The reported user for report by {UserId} was not found.", reportingUser.Id);

            return ResultT<ReportDto>.Failure(Error.NotFound("404", "The user to report was not found."));
        }

        if (await reportRepository.HasPendingReportAsync(reportingUser.Id, reportedUser.Id, request.MessageId, cancellationToken))
        {
            logger.LogWarning("User {UserId} already has a pending report for this target.", reportingUser.Id);

            return ResultT<ReportDto>.Failure(
                Error.Conflict("409", "You already have a pending report for this. It is still being reviewed.")
            );
        }

        var report = new Report
        {
            ReportId = Guid.NewGuid(),
            ReportedById = reportingUser.Id,
            ReportedUserId = reportedUser.Id,
            ReportType = type.ToString(),
            MessageId = message?.MessageId,
            ReportStatus = ReportStatus.Pending.ToString(),
            Note = request.Note,
            ReportedContent = content,
            ReportedContentType = contentType
        };

        await reportRepository.CreateAsync(report, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "{Type} report '{ReportId}' created by user '{UserId}' against user '{ReportedUserId}'.",
            type, report.ReportId, reportingUser.Id, reportedUser.Id);

        return ResultT<ReportDto>.Success(report.ToReportDto(message, reportingUser, reportedUser));
    }
}
