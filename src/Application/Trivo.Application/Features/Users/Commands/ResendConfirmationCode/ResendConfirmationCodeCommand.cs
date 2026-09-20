using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.ResendConfirmationCode;

public sealed record ResendConfirmationCodeCommand(
    string Email
) : ICommand<string>;
