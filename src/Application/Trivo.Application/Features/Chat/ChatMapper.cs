using Trivo.Application.Features.Chat.Commands.CreateChat;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Chat;
using Trivo.Application.DTOs.Users;

namespace Trivo.Application.Features.Chat;

public static class ChatMapper
{
    public static Domain.Models.Chat ToEntity(this CreateChatCommand command, User sender, User receiver)
    {
        return new Domain.Models.Chat
        {
            Id = Guid.NewGuid(),
            ChatType = ChatType.Private.ToString(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            ChatUsers = 
            [
                new ChatUser
                {
                    UserId = sender.Id,
                    JoinedAt = DateTime.UtcNow,
                    User = sender,
                    ChatName = $"{receiver.FirstName} {receiver.LastName}".Trim()
                },
                new ChatUser
                {
                    UserId = receiver.Id,
                    JoinedAt = DateTime.UtcNow,
                    User = receiver,
                    ChatName = $"{sender.FirstName} {sender.LastName}".Trim()
                }
            ]
        };
    }

    public static ChatDto ToDto(this Domain.Models.Chat entity, Guid currentUserId)
    {
        var userContext = entity.ChatUsers?
            .FirstOrDefault(cu => cu.UserId == currentUserId);

        var participants = entity.ChatUsers?
            .Select(cu => new UserChatDto(
                UserId: cu.UserId,
                Username: cu.User?.Username ?? string.Empty,
                FullName: $"{cu.User?.FirstName} {cu.User?.LastName}".Trim(),
                ProfilePicture: cu.User?.ProfilePicture
            )).ToList() ?? [];

        return new ChatDto
        (
            Id: entity.Id,
            Participants: participants,
            CreatedAt: entity.CreatedAt,
            Name: userContext?.ChatName ?? "New Chat",
            LastMessage: entity.Messages.FirstOrDefault()?.ToMessageDto()
        );
    }

    // Callers load at most the latest message into Chat.Messages (see ChatRepository.GetChatsByUserIdPagedAsync).
    private static MessageDto ToMessageDto(this Message message)
    {
        return new MessageDto(
            MessageId: message.MessageId ?? Guid.Empty,
            ChatId: message.ChatId ?? Guid.Empty,
            Content: message.Content,
            Status: message.Status,
            SentDate: message.SentAt,
            SenderId: message.SenderId ?? Guid.Empty,
            ReceiverId: message.ReceiverId,
            MessageType: message.Type
        );
    }
}