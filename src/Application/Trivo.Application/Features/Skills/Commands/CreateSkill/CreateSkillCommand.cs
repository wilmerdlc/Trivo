using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Skills.Commands.CreateSkill;

public sealed record CreateSkillCommand(string Name, Guid UserId) : ICommand<SkillDto>;