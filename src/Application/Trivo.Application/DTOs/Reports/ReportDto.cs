namespace Trivo.Application.DTOs.Reports;

public sealed record ReportDto(
    Guid ReportId,
    Guid? ReportedById,
    Guid? MessageId,
    string? Note,
    string? ReportStatus,
    MessageReportDto? Message,
    UserReportDto ReportedByUser,
    UserReportDto? ReportedUser,
    string? ReportType = null,
    Guid? ReportedUserId = null,
    DateTime? CreatedAt = null
);
