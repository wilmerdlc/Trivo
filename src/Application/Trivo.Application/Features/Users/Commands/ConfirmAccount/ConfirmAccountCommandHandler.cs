using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Users.Commands.ConfirmAccount;

internal sealed class ConfirmAccountCommandHandler(
    IUserRepository userRepository,
    ICodeService codeService,
    ILogger<ConfirmAccountCommandHandler> logger
) : ICommandHandler<ConfirmAccountCommand, string>
{
    public async Task<ResultT<string>> Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("No user was found with ID '{UserId}'.", request.UserId);

            return ResultT<string>.Failure(Error.NotFound("404", "User not found."));
        }

        var accountConfirmed = await codeService.ConfirmAccountAsync(user.Id, request.Code, cancellationToken);
        if (!accountConfirmed.IsSuccess)
        {
            logger.LogWarning(
                "Failed to confirm the account for user with ID '{UserId}': {ErrorMessage}",
                user.Id,
                accountConfirmed.Error!.Description
            );

            return ResultT<string>.Failure(accountConfirmed.Error!);
        }

        logger.LogInformation("The account for user with ID '{UserId}' was confirmed successfully.", user.Id);

        return ResultT<string>.Success("The account has been confirmed successfully.");
    }
}
