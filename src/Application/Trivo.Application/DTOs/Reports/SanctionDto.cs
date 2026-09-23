namespace Trivo.Application.DTOs.Reports;

public sealed record SanctionDto(
    Guid SanctionId,
    Guid? UserId,
    Guid? ReportId,
    Guid? AdminId,
    string? Type,
    string? Reason,
    DateTime CreatedAt,
    DateTime? ExpiresAt,
    DateTime? RevokedAt,
    // True while the sanction restricts the account (never for warnings).
    bool IsActive,
    UserReportDto? User,
    string? AdminUsername
);
