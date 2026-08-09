using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Expert;

namespace Trivo.Application.Features.Experts.Commands.UpdateExpert;

internal sealed class UpdateExpertCommandHandler(
    IExpertRepository expertRepository,
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

        expert.AvailableForProjects = request.AvailableForProjects;
        expert.IsHired = request.Hired;

        await expertRepository.UpdateAsync(expert, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Expert '{ExpertId}' updated successfully.", expert.Id);

        return ResultT<ExpertDto>.Success(expert.ToExpertDto());
    }
}
