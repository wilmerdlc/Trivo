using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUsersByInterestsAndSkills;

public sealed record GetUsersByInterestsAndSkillsQuery(
    int PageNumber,
    int PageSize,
    List<Guid> SkillIds,
    List<Guid> InterestIds
) : IQuery<PagedResult<UserAiRecommendationDto>>;
