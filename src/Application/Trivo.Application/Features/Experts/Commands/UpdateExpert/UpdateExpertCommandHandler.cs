using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Expert;

namespace Trivo.Application.Features.Experts.Commands.UpdateExpert;

internal sealed class UpdateExpertCommandHandler(
    IExpertRepository expertRepository,
    ICacheService cache,
    IUnitOfWork unitOfWork,
    ILogger<UpdateExpertCommandHandler> logger
) : ICommandHandler<UpdateExpertCommand, ExpertDto>
{
    public async Task<ResultT<ExpertDto>> Handle(UpdateExpertCommand request, CancellationToken cancellationToken)
    {
        var expert = await expertRepository.GetByIdAsync(request.ExpertId, cancellationToken);
        if (expert is null)
        {
            logger.LogError("Expert with ID {ExpertId} was not found.", request.ExpertId);

            return ResultT<ExpertDto>.Failure(Error.NotFound("404", "The expert does not exist."));
        }

        if (expert.UserId != request.RequesterId)
        {
            logger.LogWarning(
                "User {RequesterId} attempted to update expert {ExpertId}, which belongs to a different user.",
                request.RequesterId, request.ExpertId);

            return ResultT<ExpertDto>.Failure(Error.Unauthorized("403", "You can only update your own expert profile."));
        }

        expert.AvailableForProjects = request.AvailableForProjects;
        expert.IsHired = request.Hired;

        await expertRepository.UpdateAsync(expert, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await cache.InvalidateByTagsAsync([CacheKeys.UserTag(expert.UserId!.Value)], cancellationToken);

        logger.LogInformation("Expert '{ExpertId}' updated successfully.", expert.Id);

        return ResultT<ExpertDto>.Success(expert.ToExpertDto());
    }
}
