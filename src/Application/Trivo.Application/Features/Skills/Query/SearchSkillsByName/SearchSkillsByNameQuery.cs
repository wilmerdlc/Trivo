using Trivo.Application.Abstractions.Messages;
using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Skills.Query.SearchSkillsByName;

public sealed record SearchSkillsByNameQuery(string Name) : IQuery<IEnumerable<SkillWithIdDto>>;