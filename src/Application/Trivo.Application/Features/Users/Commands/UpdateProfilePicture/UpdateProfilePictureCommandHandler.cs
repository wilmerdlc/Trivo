using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Users.Commands.UpdateProfilePicture;

internal sealed class UpdateProfilePictureCommandHandler(
    IUserRepository userRepository,
    ICloudinaryService cloudinaryService,
    ICacheService cache,
    IUnitOfWork unitOfWork,
    ILogger<UpdateProfilePictureCommandHandler> logger
) : ICommandHandler<UpdateProfilePictureCommand, string>
{
    public async Task<ResultT<string>> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found."));
        }

        await using var stream = request.Image.OpenReadStream();

        var imageUrl = await cloudinaryService.UploadImageAsync(
            stream,
            request.Image.FileName,
            cancellationToken
        );

        user.ProfilePicture = imageUrl;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.InvalidateByTagsAsync([CacheKeys.UserTag(user.Id)], cancellationToken);

        logger.LogInformation("Profile picture updated successfully for user with ID '{UserId}'.", user.Id);

        return ResultT<string>.Success("Profile picture updated successfully.");
    }
}
