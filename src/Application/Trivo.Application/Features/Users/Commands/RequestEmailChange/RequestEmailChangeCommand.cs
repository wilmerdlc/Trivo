using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Users.Commands.RequestEmailChange;

public sealed record RequestEmailChangeCommand(
    Guid UserId,
    string NewEmail
) : ICommand<string>, IUserOwnedRequest;
