using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Users.Commands.UpdateBiography;

internal sealed class UpdateBiographyCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateBiographyCommandHandler> logger
) : ICommandHandler<UpdateBiographyCommand, string>
{
    public async Task<ResultT<string>> Handle(UpdateBiographyCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<string>.Failure(Error.NotFound("404", "The specified user was not found."));
        }

        user.Biography = request.Biography;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("The biography for user with ID {UserId} was updated successfully.", request.UserId);

        return ResultT<string>.Success("Biography updated successfully.");
    }
}
