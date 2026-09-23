namespace Trivo.Application.DTOs.Administrator;

public sealed record AdminRecruiterDto(
    Guid RecruiterId,
    Guid? UserId,
    string? FirstName,
    string? LastName,
    string? Username,
    string? Email,
    string? CompanyName,
    string? UserStatus,
    DateTime CreatedAt
);
