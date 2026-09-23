namespace Trivo.Application.DTOs.Reports;

public sealed record ReportDetailDto(
    Guid ReportId,
    string? ReportType,
    string? ReportStatus,
    string? Note,
    string? ReportedContent,
    string? ReportedContentType,
    Guid? MessageId,
    string? FinalReason,
    DateTime CreatedAt,
    DateTime? ReviewedAt,
    Guid? ReviewedByAdminId,
    string? ReviewedByAdminUsername,
    ReportUserDetailDto? ReportedByUser,
    ReportUserDetailDto? ReportedUser,
    SanctionDto? Sanction
);
