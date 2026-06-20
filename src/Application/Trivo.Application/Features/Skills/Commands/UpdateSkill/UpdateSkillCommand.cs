using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Skills.Commands.UpdateSkill;

public sealed record UpdateSkillCommand(
    Guid UserId,
    List<Guid> SkillIds
) : ICommand<string>;