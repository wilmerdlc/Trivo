namespace Trivo.Application.DTOs.Recruiter;

public sealed record RecruiterDto(
    Guid Id,
    string CompanyName,
    Guid UserId);
