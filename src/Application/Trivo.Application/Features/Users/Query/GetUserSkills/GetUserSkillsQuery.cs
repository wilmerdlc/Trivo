using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Users.Query.GetUserSkills;

public sealed record GetUserSkillsQuery(Guid UserId) : IQuery<IEnumerable<SkillWithIdDto>>;
