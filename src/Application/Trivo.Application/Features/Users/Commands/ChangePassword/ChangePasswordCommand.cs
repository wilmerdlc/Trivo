using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.ChangePassword;

public sealed record ChangePasswordCommand(
    string Email,
    string NewPassword,
    string ConfirmPassword
) : ICommand<string>;
