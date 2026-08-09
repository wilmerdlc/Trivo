using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Expert;

namespace Trivo.Application.Features.Experts.Commands.CreateExpert;

internal sealed class CreateExpertCommandHandler(
    IExpertRepository expertRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateExpertCommandHandler> logger
) : ICommandHandler<CreateExpertCommand, ExpertDto>
{
    public async Task<ResultT<ExpertDto>> Handle(CreateExpertCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogError("The user with id {UserId} does not exist.", request.UserId);

            return ResultT<ExpertDto>.Failure(Error.NotFound("404", "The user does not exist"));
        }

        var expert = request.ToExpertEntity(Guid.NewGuid());

        await expertRepository.CreateAsync(expert, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Expert '{ExpertId}' created successfully for user '{UserId}'.", expert.Id, request.UserId);

        return ResultT<ExpertDto>.Success(expert.ToExpertDto());
    }
}
