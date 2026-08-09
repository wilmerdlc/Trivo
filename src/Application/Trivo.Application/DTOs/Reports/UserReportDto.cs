namespace Trivo.Application.DTOs.Reports;

public sealed record UserReportDto(
    Guid UserId,
    string? FirstName,
    string? LastName
);
