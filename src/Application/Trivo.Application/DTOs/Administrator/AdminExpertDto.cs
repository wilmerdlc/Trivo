namespace Trivo.Application.DTOs.Administrator;

public sealed record AdminExpertDto(
    Guid ExpertId,
    Guid? UserId,
    string? FirstName,
    string? LastName,
    string? Username,
    string? Email,
    string? Position,
    bool? AvailableForProjects,
    bool? IsHired,
    string? UserStatus,
    DateTime CreatedAt
);
