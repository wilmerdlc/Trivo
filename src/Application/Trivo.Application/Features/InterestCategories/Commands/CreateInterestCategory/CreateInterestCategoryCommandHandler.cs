using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Features.InterestCategories;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.InterestCategories;

namespace Trivo.Application.Features.InterestCategories.Commands.CreateInterestCategory;

internal sealed class CreateInterestCategoryCommandHandler(
    ILogger<CreateInterestCategoryCommandHandler> logger,
    IInterestCategoryRepository interestCategoryRepository,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<CreateInterestCategoryCommand, InterestCategoryDto>
{
    public async Task<ResultT<InterestCategoryDto>> Handle(CreateInterestCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            logger.LogWarning("The request to create the interest category is null.");

            return ResultT<InterestCategoryDto>.Failure(Error.Failure("400",
                "Cannot create the interest category. The request is invalid."));
        }

        InterestCategory interestCategory = request.ToEntity();

        await interestCategoryRepository.CreateAsync(interestCategory, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully created interest category with ID {CategoryId} and name '{Name}'.",
            interestCategory.CategoryId, interestCategory.Name);

        return ResultT<InterestCategoryDto>.Success(interestCategory.ToDto());
    }
}