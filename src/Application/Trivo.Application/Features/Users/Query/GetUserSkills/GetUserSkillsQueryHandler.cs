using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Skills;

namespace Trivo.Application.Features.Users.Query.GetUserSkills;

internal sealed class GetUserSkillsQueryHandler(
    ILogger<GetUserSkillsQueryHandler> logger,
    IUserRepository userRepository,
    ICacheService cache
) : IQueryHandler<GetUserSkillsQuery, IEnumerable<SkillWithIdDto>>
{
    public async Task<ResultT<IEnumerable<SkillWithIdDto>>> Handle(GetUserSkillsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found to retrieve their skills.");

            return ResultT<IEnumerable<SkillWithIdDto>>.Failure(Error.NotFound("404", "The user does not exist"));
        }

        var skills = await cache.GetOrSetAsync(
            CacheKeys.UserSkills(request.UserId),
            async () =>
            {
                var userSkills = await userRepository.GetSkillsByUserIdAsync(request.UserId, cancellationToken);

                return UserMapper.MapToSkills(userSkills.ToList());
            },
            CacheProfiles.Warm with { Tags = [CacheKeys.UserTag(request.UserId)] },
            cancellationToken
        );

        if (!skills.Any())
        {
            logger.LogInformation("User with ID {UserId} has no registered skills.", request.UserId);

            return ResultT<IEnumerable<SkillWithIdDto>>.Failure(
                Error.Failure("404", "The user has no registered skills.")
            );
        }

        logger.LogInformation("Retrieved {Count} skills for user with ID {UserId}.", skills.Count, request.UserId);

        return ResultT<IEnumerable<SkillWithIdDto>>.Success(skills);
    }
}
