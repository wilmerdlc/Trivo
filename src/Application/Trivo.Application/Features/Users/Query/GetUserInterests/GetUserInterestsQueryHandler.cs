using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Users.Query.GetUserInterests;

internal sealed class GetUserInterestsQueryHandler(
    ILogger<GetUserInterestsQueryHandler> logger,
    IUserRepository userRepository,
    ICacheService cache
) : IQueryHandler<GetUserInterestsQuery, IEnumerable<InterestWithIdDto>>
{
    public async Task<ResultT<IEnumerable<InterestWithIdDto>>> Handle(GetUserInterestsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found to retrieve their interests.");

            return ResultT<IEnumerable<InterestWithIdDto>>.Failure(Error.NotFound("404", "The user does not exist"));
        }

        var interests = await cache.GetOrSetAsync(
            CacheKeys.UserInterests(request.UserId),
            async () =>
            {
                var userInterests = await userRepository.GetInterestsByUserIdAsync(request.UserId, cancellationToken);

                return UserMapper.MapToInterests(userInterests.ToList());
            },
            CacheProfiles.Warm with { Tags = [CacheKeys.UserTag(request.UserId)] },
            cancellationToken
        );

        if (!interests.Any())
        {
            logger.LogWarning("User with ID {UserId} has no registered interests.", request.UserId);

            return ResultT<IEnumerable<InterestWithIdDto>>.Failure(
                Error.Failure("404", "The user has no registered interests.")
            );
        }

        logger.LogInformation("Retrieved {Count} interests for user with ID {UserId}.", interests.Count, request.UserId);

        return ResultT<IEnumerable<InterestWithIdDto>>.Success(interests);
    }
}
