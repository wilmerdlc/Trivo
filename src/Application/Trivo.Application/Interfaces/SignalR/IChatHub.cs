
using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Interfaces.SignalR;

public interface IChatHub
{
    Task ReceivePrivateMessage(MessageDto message);
    
    Task ReceiveChats(IEnumerable<ChatDto> chats);
    
    Task ReceiveNewChat(ChatDto chat);
    
    Task ReceiveChatMessages(Guid chatId, IEnumerable<MessageDto> messages);
    
    Task NotifyNewMatch(string message);
    
    Task NotifyPendingMatch(string message);
}