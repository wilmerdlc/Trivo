using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.ResetPassword;

public sealed record ResetPasswordCommand(
    Guid UserId,
    string Code,
    string NewPassword,
    string ConfirmPassword
) : ICommand<string>;
