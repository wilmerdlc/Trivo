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

    public static ReportDto ToReportDto(this Report report, Message message, User reportedByUser, User? reportedUser)
    {
        return new ReportDto(
            ReportId: report.ReportId ?? Guid.Empty,
            ReportedById: report.ReportedById,
            MessageId: report.MessageId,
            Note: report.Note,
            ReportStatus: report.ReportStatus,
            Message: message.ToMessageReportDto(),
            ReportedByUser: reportedByUser.ToUserReportDto(),
            ReportedUser: reportedUser?.ToUserReportDto()
        );
    }
}
