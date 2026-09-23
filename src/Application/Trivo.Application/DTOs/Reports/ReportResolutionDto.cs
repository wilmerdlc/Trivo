namespace Trivo.Application.DTOs.Reports;

public sealed record ReportResolutionDto(
    Guid ReportId,
    string ReportStatus,
    string FinalReason,
    DateTime ReviewedAt,
    SanctionDto? Sanction
);
