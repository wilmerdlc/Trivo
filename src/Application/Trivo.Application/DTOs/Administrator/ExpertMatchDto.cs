using Trivo.Application.DTOs.Administrator;

namespace Trivo.Application.DTOs.Administrator;

public sealed record ExpertMatchDto(
    string? FirstName,
    string LastName
);