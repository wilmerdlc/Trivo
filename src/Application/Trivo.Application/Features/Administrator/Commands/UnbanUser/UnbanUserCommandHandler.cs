using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Administrator.Commands.UnbanUser;

internal sealed class UnbanUserCommandHandler(
    ILogger<UnbanUserCommandHandler> logger,
    IAdministratorRepository adminRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UnbanUserCommand, string>
{
    public async Task<ResultT<string>> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            logger.LogInformation("User with ID {UserId} was not found.", request.UserId);

            return ResultT<string>.Failure(
                Error.NotFound("404", "User not found.")
            );
        }

        await adminRepository.UnbanAsync(request.UserId, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("User with ID {UserId} has been unbanned.", user.Id);

        return ResultT<string>.Success(
            $"User {user.FirstName} - {user.Id} has been unbanned."
        );
    }
}