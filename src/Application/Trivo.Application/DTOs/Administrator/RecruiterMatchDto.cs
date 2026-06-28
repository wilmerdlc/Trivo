using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.DTOs.Administrator;

public sealed record RecruiterMatchDto(
    string? FirstName,
    string LastName
);