using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Query.GetUserProfilePicture;

internal sealed class GetUserProfilePictureQueryHandler(
    ILogger<GetUserProfilePictureQueryHandler> logger,
    IUserRepository userRepository,
    IDistributedCache cache
) : IQueryHandler<GetUserProfilePictureQuery, UserProfilePictureDto>
{
    public async Task<ResultT<UserProfilePictureDto>> Handle(GetUserProfilePictureQuery request, CancellationToken cancellationToken)
    {
        var profilePicture = await cache.GetOrCreateAsync(
            $"user-profile-picture-{request.UserId}",
            async () =>
            {
                var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

                return user is null ? null : new UserProfilePictureDto(user.ProfilePicture);
            },
            cancellationToken: cancellationToken
        );

        if (profilePicture is null)
        {
            logger.LogWarning("No user was found to retrieve their profile picture.");

            return ResultT<UserProfilePictureDto>.Failure(Error.NotFound("404", "The user does not exist"));
        }

        logger.LogInformation("Successfully retrieved the profile picture for user with ID '{UserId}'.", request.UserId);

        return ResultT<UserProfilePictureDto>.Success(profilePicture);
    }
}
