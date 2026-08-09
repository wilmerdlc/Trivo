using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Interests.Commands.UpdateInterest;

internal sealed class UpdateInterestCommandHandler(
    ILogger<UpdateInterestCommandHandler> logger,
    IInterestRepository interestRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateInterestCommand, string>
{
    public async Task<ResultT<string>> Handle(UpdateInterestCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("User not found with ID: {UserId}", request.UserId);

            return ResultT<string>.Failure(
                Error.NotFound("404", "The user was not found."));
        }

        await interestRepository.UpdateUserInterestsAsync(request.UserId, request.InterestIds.ToList(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Interests for user {UserId} successfully updated.", request.UserId);

        return ResultT<string>.Success("Interests updated successfully.");
    }
}