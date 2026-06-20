using Trivo.Application.DTOs.Skills;
using Trivo.Application.Features.Skills.Commands.CreateSkill;
using Trivo.Domain.Models;

namespace Trivo.Application.Features.Skills;

public static class SkillMapper
{
    public static SkillDto ToSkillDto(this Skill skill) =>
        new(
            SkillId: skill.SkillId ?? Guid.Empty,
            Name: skill.Name ?? string.Empty,
            RegisteredAt: skill.RegisteredAt
        );

    public static IEnumerable<SkillDto> ToSkillDtoList(this IEnumerable<Skill> skills) =>
        skills.Select(ToSkillDto).ToList();

    public static Skill ToSkillEntity(this CreateSkillCommand command, Guid skillId) =>
        new()
        {
            SkillId = skillId,
            Name = command.Name
        };

    public static UserSkill ToUserSkillEntity(this CreateSkillCommand command, Guid skillId) =>
        new()
        {
            UserId = command.UserId,
            SkillId = skillId
        };
}