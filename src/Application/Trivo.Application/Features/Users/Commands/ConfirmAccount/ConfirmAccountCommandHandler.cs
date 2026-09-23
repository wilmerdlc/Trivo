using Microsoft.Extensions.Logging;
using Trivo.Application.Abstractions.Messages;
using Trivo.Application.Interfaces.Services;
using Trivo.Application.Utils;

namespace Trivo.Application.Features.Users.Commands.ConfirmAccount;

internal sealed class ConfirmAccountCommandHandler(
    ICodeService codeService,
    ILogger<ConfirmAccountCommandHandler> logger
) : ICommandHandler<ConfirmAccountCommand, string>
{
    public async Task<ResultT<string>> Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        var accountConfirmed = await codeService.ConfirmAccountAsync(request.UserId, request.Code, cancellationToken);
        if (!accountConfirmed.IsSuccess)
        {
            logger.LogWarning(
                "Failed to confirm the account for user with ID '{UserId}': {ErrorMessage}",
                request.UserId,
                accountConfirmed.Error!.Description
            );

            return ResultT<string>.Failure(accountConfirmed.Error!);
        }

        logger.LogInformation("The account was confirmed successfully with code for user ID '{UserId}'.", request.UserId);

        return ResultT<string>.Success("The account has been confirmed successfully.");
    }
}
