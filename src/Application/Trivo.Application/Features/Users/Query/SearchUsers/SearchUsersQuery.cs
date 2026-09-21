using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.SearchUsers;

public sealed record SearchUsersQuery(
    Guid UserId,
    string Text,
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<UserAiRecommendationDto>>, IUserOwnedRequest;
