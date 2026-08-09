using Trivo.Application.Abstractions.Messages;

namespace Trivo.Application.Features.Administrator.Commands.UnbanUser;

public sealed record UnbanUserCommand(
    Guid UserId
) : ICommand<string>;