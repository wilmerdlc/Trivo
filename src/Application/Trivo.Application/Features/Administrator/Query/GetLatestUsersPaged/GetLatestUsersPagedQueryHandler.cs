using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Features.Users;
using Trivo.Application.Pagination;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Administrator.Query.GetLatestUsersPaged;

internal sealed class GetLatestUsersPagedQueryHandler(
    ILogger<GetLatestUsersPagedQueryHandler> logger,
    IAdministratorRepository adminRepository,
    ICacheService cache
) : IQueryHandler<GetLatestUsersPagedQuery, PagedResult<UserDto>>
{
    public async Task<ResultT<PagedResult<UserDto>>> Handle(
        GetLatestUsersPagedQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The request to retrieve latest registered users was null.");

            return ResultT<PagedResult<UserDto>>.Failure(
                Error.Failure("400", "Request cannot be null."));
        }

        if (request.PageNumber <= 0 || request.PageSize <= 0)
        {
            logger.LogWarning(
                "Invalid pagination parameters: PageNumber ({PageNumber}) or PageSize ({PageSize}) are not valid.",
                request.PageNumber,
                request.PageSize);

            return ResultT<PagedResult<UserDto>>.Failure(
                Error.Failure("400", "Page number and page size must be greater than zero."));
        }

        var pagedResult = await cache.GetOrSetAsync(
            CacheKeys.AdminLatestUsers(request.PageNumber, request.PageSize),
            async () =>
            {
                var result = await adminRepository.GetPagedLatestUsersAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

                IEnumerable<UserDto> dtoList = result.Items!
                    .Select(UserMapper.MapUserDto)
                    .ToList();

                var paginated = new PagedResult<UserDto>(
                    items: dtoList,
                    totalItems: result.TotalItems,
                    currentPage: request.PageNumber,
                    pageSize: request.PageSize
                );

                return paginated;
            },
            CacheProfiles.Hot with { Tags = [CacheKeys.AdminUsersTag] },
            cancellationToken
        );

        logger.LogInformation(
            "Latest users retrieved successfully. Count={Count} Page={Page} Size={Size}",
            pagedResult.Items!.Count(),
            request.PageNumber,
            request.PageSize);

        return ResultT<PagedResult<UserDto>>.Success(pagedResult);
    }
}