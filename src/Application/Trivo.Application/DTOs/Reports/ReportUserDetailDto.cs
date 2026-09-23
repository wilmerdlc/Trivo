namespace Trivo.Application.DTOs.Reports;

/// <summary>
/// Full profile of a user involved in a report, so the administrator can review the case without
/// leaving the report screen.
/// </summary>
public sealed record ReportUserDetailDto(
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? Username,
    string? Email,
    string? Biography,
    string? Position,
    string? Location,
    string? ProfilePicture,
    string? LinkedIn,
    string? UserStatus,
    string Role,
    bool IsAccountConfirmed,
    DateTime CreatedAt
);
