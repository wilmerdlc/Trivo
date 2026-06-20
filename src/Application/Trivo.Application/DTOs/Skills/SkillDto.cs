namespace Trivo.Application.DTOs.Skills;

public record SkillDto(
    Guid? SkillId,
    string? Name,
    DateTime? RegisteredAt
);