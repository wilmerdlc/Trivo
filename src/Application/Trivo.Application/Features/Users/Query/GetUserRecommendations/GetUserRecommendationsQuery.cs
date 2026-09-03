using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserRecommendations;

public sealed record GetUserRecommendationsQuery(
    Guid UserId,
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<UserAiRecommendationDto>>, IUserOwnedRequest;
