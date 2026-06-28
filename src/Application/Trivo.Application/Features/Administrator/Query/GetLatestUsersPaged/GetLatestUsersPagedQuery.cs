using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged;

public sealed record GetLatestUsersPagedQuery(
    int PageNumber,
    int PageSize
) : IQuery<PagedResult<UserDto>>;