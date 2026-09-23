namespace Trivo.Application.DTOs.Reports;

public sealed record ReportListItemDto(
    Guid ReportId,
    string? ReportType,
    string? ReportStatus,
    string? Note,
    DateTime CreatedAt,
    DateTime? ReviewedAt,
    UserReportDto? ReportedByUser,
    UserReportDto? ReportedUser
);
