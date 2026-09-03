using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Messages.Commands.SendMessage;

public sealed record SendMessageCommand(
    Guid ChatId,
    Guid SenderId,
    Guid ReceiverId,
    string Content
) : ICommand<MessageDto>, IUserOwnedRequest
{
    Guid IUserOwnedRequest.UserId => SenderId;
}
