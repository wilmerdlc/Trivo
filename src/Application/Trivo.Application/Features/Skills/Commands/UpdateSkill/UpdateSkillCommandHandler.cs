using MediatR;
using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Features.Users.Events;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Skills.Commands.UpdateSkill;

internal sealed class UpdateSkillCommandHandler(
    ILogger<UpdateSkillCommandHandler> logger,
    ISkillRepository skillRepository,
    IUserRepository userRepository,
    IPublisher publisher,
    ICacheService cache,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateSkillCommand, string>
{
    public async Task<ResultT<string>> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        if (request.SkillIds.Count == 0)
        {
            logger.LogWarning("The skill list provided is empty. UserId: {UserId}", request.UserId);

            return ResultT<string>.Failure(
                Error.Failure("400", "At least one skill must be provided to update."));
        }

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("User with ID {UserId} was not found.", request.UserId);

            return ResultT<string>.Failure(Error.NotFound("404", "The user was not found."));
        }

        await skillRepository.UpdateAsync(request.UserId, request.SkillIds, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new UserProfileChangedEvent(request.UserId), cancellationToken);
        await cache.InvalidateByTagsAsync([CacheKeys.UserTag(request.UserId)], cancellationToken);

        logger.LogInformation("Skills for user {UserId} successfully updated.", request.UserId);

        return ResultT<string>.Success("Skills updated successfully.");
    }
}