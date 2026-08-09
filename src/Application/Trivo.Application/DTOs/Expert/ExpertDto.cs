namespace Trivo.Application.DTOs.Expert;

public sealed record ExpertDto(
    Guid Id,
    bool? AvailableForProjects,
    bool? IsHired,
    Guid UserId);
