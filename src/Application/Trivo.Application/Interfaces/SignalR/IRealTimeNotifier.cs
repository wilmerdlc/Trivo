
using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Interfaces.SignalR;

public interface IRealTimeNotifier
{
    Task NotifyPrivateMessageAsync(MessageDto message, Guid userId);
    
    Task NotifyMatchConfirmedAsync(Guid userId, string content);
    
    Task NotifyMatchPendingAsync(Guid userId, string content);
    
    Task NotifyNewChatAsync(Guid userId, IEnumerable<ChatDto> chats);
    
    Task NotifyPagedChatsAsync(Guid userId, IEnumerable<ChatDto> chats);
    
    Task NotifyMessagePageAsync(Guid userId, Guid chatId, IEnumerable<MessageDto> messagePage);
}