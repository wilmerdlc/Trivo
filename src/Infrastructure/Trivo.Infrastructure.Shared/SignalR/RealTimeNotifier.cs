using Microsoft.AspNetCore.SignalR;
using Trivo.Application.Interfaces.SignalR;
using Trivo.Infrastructure.Shared.SignalR.Hubs;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Infrastructure.Shared.SignalR;

public class RealTimeNotifier(IHubContext<ChatHub, IChatHub> hub) : IRealTimeNotifier
{
    public Task NotifyPrivateMessageAsync(MessageDto message, Guid userId)
        => hub.Clients.User(userId.ToString())
            .ReceivePrivateMessage(message);

    public Task NotifyMatchConfirmedAsync(Guid userId, string content)
        => hub.Clients.User(userId.ToString())
            .NotifyNewMatch(content);

    public Task NotifyMatchPendingAsync(Guid userId, string content)
        => hub.Clients.User(userId.ToString())
            .NotifyPendingMatch(content);

    public async Task NotifyNewChatAsync(Guid userId, IEnumerable<ChatDto> chats)
    {
        foreach (var chat in chats)
        {
            await hub.Clients.User(userId.ToString())
                .ReceiveNewChat(chat);
        }
    }

    public async Task NotifyPagedChatsAsync(Guid userId, IEnumerable<ChatDto> chats)
    {
        await hub.Clients.User(userId.ToString())
            .ReceiveChats(chats);
    }

    public async Task NotifyMessagePageAsync(Guid userId, Guid chatId, IEnumerable<MessageDto> messagePage)
    {
        await hub.Clients.User(userId.ToString())
            .ReceiveChatMessages(chatId, messagePage);
    }
}