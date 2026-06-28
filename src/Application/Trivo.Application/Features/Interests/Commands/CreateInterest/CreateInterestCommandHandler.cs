using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Interests;

namespace Trivo.Application.Features.Interests.Commands.CreateInterest;

internal sealed class CreateInterestCommandHandler(
    ILogger<CreateInterestCommandHandler> logger,
    IInterestCategoryRepository interestCategoryRepository,
    IUserInterestRepository userInterestRepository,
    IInterestRepository interestRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateInterestCommand, InterestDetailsDto>
{
    public async Task<ResultT<InterestDetailsDto>> Handle(CreateInterestCommand request,
        CancellationToken cancellationToken)
    {
        var category = await interestCategoryRepository.GetByIdAsync(request.CategoryId!.Value, cancellationToken);
        if (category is null)
        {
            logger.LogWarning("No interest category found with the provided ID: {CategoryId}", request.CategoryId);

            return ResultT<InterestDetailsDto>.Failure(
                Error.NotFound("404", "The specified interest category does not exist or the ID is invalid.")
            );
        }

        // Same name cannot exist in the specified category
        if (await interestRepository.ExistsByNameAndCategoryAsync(request.Name, request.CategoryId.Value,
                cancellationToken))
        {
            logger.LogWarning("An interest with name '{Name}' already exists in category {CategoryId}",
                request.Name, request.CategoryId);

            return ResultT<InterestDetailsDto>.Failure(
                Error.Conflict("409", "This interest already exists in the specified category.")
            );
        }

        Interest interestEntity = request.ToInterestEntity(Guid.NewGuid());

        UserInterest userInterest = request.ToUserInterestEntity(Guid.NewGuid());

        await interestRepository.AddAsync(interestEntity, cancellationToken);
        await userInterestRepository.AddAsync(userInterest, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Interest '{Name}' created with ID {InterestId}.", interestEntity.Name,
            interestEntity.Id);

        return ResultT<InterestDetailsDto>.Success(interestEntity.ToInterestDetailsDto());
    }
}