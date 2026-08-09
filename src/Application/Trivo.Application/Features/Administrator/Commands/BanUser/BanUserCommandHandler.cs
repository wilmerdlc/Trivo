using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Administrator.Commands.BanUser;

internal sealed class BanUserCommandHandler(
    ILogger<BanUserCommandHandler> logger,
    IAdministratorRepository administratorRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<BanUserCommand, string>
{
    public async Task<ResultT<string>> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            logger.LogInformation("User with ID {UserId} was not found", request.UserId);

            return ResultT<string>.Failure(
                Error.NotFound("User.NotFound", "The specified user does not exist.")
            );
        }

        await administratorRepository.BanAsync(request.UserId, cancellationToken);

        logger.LogInformation("User with ID {UserId} has been banned", user.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultT<string>.Success(
            $"User {user.FirstName} {user.LastName} - {user.Id} has been banned successfully"
        );
    }
}