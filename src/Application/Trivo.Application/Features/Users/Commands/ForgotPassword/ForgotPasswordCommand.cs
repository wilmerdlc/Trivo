using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(
    string Email
) : ICommand<string>;
