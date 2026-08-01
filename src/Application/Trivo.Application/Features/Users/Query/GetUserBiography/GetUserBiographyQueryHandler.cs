using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserBiography;

internal sealed class GetUserBiographyQueryHandler(
    ILogger<GetUserBiographyQueryHandler> logger,
    IUserRepository userRepository,
    IDistributedCache cache
) : IQueryHandler<GetUserBiographyQuery, UserBiographyDto>
{
    public async Task<ResultT<UserBiographyDto>> Handle(GetUserBiographyQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"user-biography-{request.UserId}";

        var biography = await cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

                return user is null ? null : new UserBiographyDto(user.Biography);
            },
            cancellationToken: cancellationToken
        );

        if (biography is null)
        {
            logger.LogWarning("No user was found to retrieve their biography.");

            return ResultT<UserBiographyDto>.Failure(Error.NotFound("404", "The user does not exist"));
        }

        return ResultT<UserBiographyDto>.Success(biography);
    }
}
