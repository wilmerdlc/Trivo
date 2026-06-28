using Trivo.Application.Abstractions.Messages;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Features.Chat.Commands.CreateChat;

public sealed record CreateChatCommand(
    Guid SenderId,
    Guid ReceiverId
): ICommand<ChatDto>;