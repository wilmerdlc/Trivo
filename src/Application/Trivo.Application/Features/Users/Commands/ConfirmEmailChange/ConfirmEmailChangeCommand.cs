using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.ConfirmEmailChange;

public sealed record ConfirmEmailChangeCommand(
    Guid UserId,
    string Code
) : ICommand<string>, IUserOwnedRequest;
