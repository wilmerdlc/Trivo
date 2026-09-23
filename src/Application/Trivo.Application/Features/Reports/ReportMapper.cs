using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Reports;

namespace Trivo.Application.Features.Reports;

public static class ReportMapper
{
    public static UserReportDto ToUserReportDto(this User user)
    {
        return new UserReportDto(
            UserId: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName
        );
    }

    public static MessageReportDto ToMessageReportDto(this Message message)
    {
        return new MessageReportDto(
            MessageId: message.MessageId ?? Guid.Empty,
            SenderId: message.SenderId,
            Content: message.Content,
            Type: message.Type,
            SentAt: message.SentAt,
            Sender: message.Sender?.ToUserReportDto()
        );
    }

    public static ReportDto ToReportDto(this Report report, Message? message, User reportedByUser, User? reportedUser)
    {
        return new ReportDto(
            ReportId: report.ReportId ?? Guid.Empty,
            ReportedById: report.ReportedById,
            MessageId: report.MessageId,
            Note: report.Note,
            ReportStatus: report.ReportStatus,
            Message: message?.ToMessageReportDto(),
            ReportedByUser: reportedByUser.ToUserReportDto(),
            ReportedUser: reportedUser?.ToUserReportDto(),
            ReportType: report.ReportType,
            ReportedUserId: report.ReportedUserId,
            CreatedAt: report.CreatedAt
        );
    }

    public static ReportListItemDto ToListItemDto(this Report report)
    {
        return new ReportListItemDto(
            ReportId: report.ReportId ?? Guid.Empty,
            ReportType: report.ReportType,
            ReportStatus: report.ReportStatus,
            Note: report.Note,
            CreatedAt: report.CreatedAt,
            ReviewedAt: report.ReviewedAt,
            ReportedByUser: report.Reporter?.ToUserReportDto(),
            ReportedUser: report.ReportedUser?.ToUserReportDto()
        );
    }

    public static ReportUserDetailDto ToUserDetailDto(this User user)
    {
        var role = user.Experts is { Count: > 0 }
            ? Roles.Expert.ToString()
            : user.Recruiters is { Count: > 0 }
                ? Roles.Recruiter.ToString()
                : "No Role";

        return new ReportUserDetailDto(
            UserId: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Username: user.Username,
            Email: user.Email,
            Biography: user.Biography,
            Position: user.Position,
            Location: user.Location,
            ProfilePicture: user.ProfilePicture,
            LinkedIn: user.LinkedIn,
            UserStatus: user.UserStatus,
            Role: role,
            IsAccountConfirmed: user.IsAccountConfirmed ?? false,
            CreatedAt: user.CreatedAt
        );
    }

    public static ReportDetailDto ToDetailDto(this Report report, DateTime nowUtc)
    {
        return new ReportDetailDto(
            ReportId: report.ReportId ?? Guid.Empty,
            ReportType: report.ReportType,
            ReportStatus: report.ReportStatus,
            Note: report.Note,
            ReportedContent: report.ReportedContent,
            ReportedContentType: report.ReportedContentType,
            MessageId: report.MessageId,
            FinalReason: report.FinalReason,
            CreatedAt: report.CreatedAt,
            ReviewedAt: report.ReviewedAt,
            ReviewedByAdminId: report.ReviewedByAdminId,
            ReviewedByAdminUsername: report.ReviewedByAdmin?.Username,
            ReportedByUser: report.Reporter?.ToUserDetailDto(),
            ReportedUser: report.ReportedUser?.ToUserDetailDto(),
            Sanction: report.Sanction?.ToSanctionDto(nowUtc)
        );
    }

    public static SanctionDto ToSanctionDto(this Sanction sanction, DateTime nowUtc)
    {
        var restricts = sanction.Type != SanctionType.Warning.ToString();
        var isActive = restricts &&
                       sanction.RevokedAt is null &&
                       (sanction.ExpiresAt is null || sanction.ExpiresAt > nowUtc);

        return new SanctionDto(
            SanctionId: sanction.Id,
            UserId: sanction.UserId,
            ReportId: sanction.ReportId,
            AdminId: sanction.AdminId,
            Type: sanction.Type,
            Reason: sanction.Reason,
            CreatedAt: sanction.CreatedAt,
            ExpiresAt: sanction.ExpiresAt,
            RevokedAt: sanction.RevokedAt,
            IsActive: isActive,
            User: sanction.User?.ToUserReportDto(),
            AdminUsername: sanction.Admin?.Username
        );
    }

    /// <summary>
    /// Plain-text snapshot of the profile fields a moderator needs to judge a profile report,
    /// captured at report time.
    /// </summary>
    public static string ToProfileSnapshot(this User user)
    {
        return string.Join('\n',
            $"Name: {user.FirstName} {user.LastName}".Trim(),
            $"Username: {user.Username}",
            $"Position: {user.Position}",
            $"Location: {user.Location}",
            $"LinkedIn: {user.LinkedIn}",
            $"ProfilePicture: {user.ProfilePicture}",
            $"Biography: {user.Biography}");
    }
}
