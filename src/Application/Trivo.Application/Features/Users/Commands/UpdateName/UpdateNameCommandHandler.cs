using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.UnitOfWork;
using Trivo.Application.Utils;

using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Users.Commands.UpdateName;

internal sealed class UpdateNameCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateNameCommandHandler> logger
) : ICommandHandler<UpdateNameCommand, UpdateNameDto>
{
    public async Task<ResultT<UpdateNameDto>> Handle(UpdateNameCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<UpdateNameDto>.Failure(Error.NotFound("404", "User not found."));
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Name updated successfully for user with ID '{UserId}'.", user.Id);

        return ResultT<UpdateNameDto>.Success(new UpdateNameDto(
            FirstName: user.FirstName,
            LastName: user.LastName
        ));
    }
}
