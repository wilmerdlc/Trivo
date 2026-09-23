using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.Users;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Administrator.Query.GetBannedUsersPaged;

internal sealed class GetBannedUsersPagedQueryHandler(
    IAdministratorRepository adminRepository,
    ILogger<GetBannedUsersPagedQueryHandler> logger
) : IQueryHandler<GetBannedUsersPagedQuery, PagedResult<UserDto>>
{
    public async Task<ResultT<PagedResult<UserDto>>> Handle(
        GetBannedUsersPagedQuery request,
        CancellationToken cancellationToken)
    {
        // Not cached: a ban or unban takes effect immediately and this list is what an
        // administrator checks right after acting on it.
        var page = await adminRepository.GetPagedBannedUsersAsync(request.PageNumber, request.PageSize, cancellationToken);

        var items = page.Items!.Select(UserMapper.MapUserDto).ToList();

        logger.LogInformation(
            "Banned users retrieved. Page={Page} Size={Size} Total={Total}",
            request.PageNumber, request.PageSize, page.TotalItems);

        return ResultT<PagedResult<UserDto>>.Success(
            new PagedResult<UserDto>(items, page.TotalItems, request.PageNumber, request.PageSize)
        );
    }
}
