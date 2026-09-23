using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Pagination;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Administrator.Query.GetBannedUsersPaged;

public sealed record GetBannedUsersPagedQuery(int PageNumber, int PageSize)
    : IQuery<PagedResult<UserDto>>;
