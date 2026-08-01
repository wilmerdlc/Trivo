using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.ConfirmAccount;

public sealed record ConfirmAccountCommand(
    Guid UserId,
    string Code
) : ICommand<string>;
