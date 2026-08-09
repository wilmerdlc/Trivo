using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Features.Users;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Administrator.Query.GetLastBannedUsers;

internal sealed class GetLast10BannedUsersQueryHandler(
    ILogger<GetLast10BannedUsersQueryHandler> logger,
    IAdministratorRepository adminRepository,
    IDistributedCache cache
) : IQueryHandler<GetLast10BannedUsersQuery, IEnumerable<UserDto>>
{
    public async Task<ResultT<IEnumerable<UserDto>>> Handle(
        GetLast10BannedUsersQuery request,
        CancellationToken cancellationToken)
    {
        var cachedUsers = await cache.GetOrCreateAsync(
            "get-last-banned-users",
            async () =>
            {
                var bannedUsers = await adminRepository.GetLast10BannedUsersAsync(cancellationToken);

                return bannedUsers
                    .Select(UserMapper.MapUserDto)
                    .ToList();
            },
            cancellationToken: cancellationToken
        );

        if (!cachedUsers!.Any())
        {
            logger.LogWarning("No banned users were found.");

            return ResultT<IEnumerable<UserDto>>.Failure(
                Error.Failure("404", "No banned users were found."));
        }

        logger.LogInformation("Retrieved {Count} banned users.", cachedUsers.Count);

        return ResultT<IEnumerable<UserDto>>.Success(cachedUsers);
    }
}