using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public class ChatRepository(TrivoContext context) : GenericRepository<Chat>(context), IChatRepository
{
    public async Task<IEnumerable<Chat>> Get10RecentChatsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<ChatUser>()
            .Where(cu => cu.UserId == userId)
            .OrderByDescending(cu => cu.Chat!.CreatedAt)
            .Select(cu => cu.Chat!)
            .Include(c => c.ChatUsers)!
            .ThenInclude(cu => cu.User)
            .Take(10)
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<Chat>> GetChatsByUserIdPagedAsync(
        Guid userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var total = await Context.Set<ChatUser>()
            .Where(cu => cu.UserId == userId)
            .CountAsync(cancellationToken);

        var chats = await Context.Set<ChatUser>()
            .Where(cu => cu.UserId == userId)
            .OrderByDescending(cu => cu.JoinedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(cu => new Chat
            {
                Id = cu.Chat!.Id,
                CreatedAt = cu.Chat.CreatedAt,
                ChatUsers = cu.Chat.ChatUsers!.Select(c => new ChatUser
                {
                    UserId = c.UserId,
                    ChatName = c.ChatName,
                    User = new User
                    {
                        Id = c.User!.Id,
                        Username = c.User.Username,
                        FirstName = c.User.FirstName,
                        LastName = c.User.LastName,
                        ProfilePicture = c.User.ProfilePicture
                    }
                }).ToList(),
                Messages = cu.Chat.Messages
                    .OrderByDescending(m => m.SentAt)
                    .Take(1)
                    .Select(m => new Message
                    {
                        MessageId = m.MessageId,
                        Content = m.Content,
                        SentAt = m.SentAt,
                        Status = m.Status,
                        SenderId = m.SenderId,
                        ChatId = m.ChatId,
                        Type = m.Type
                    }).ToList()
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PagedResult<Chat>(chats, total, page, pageSize);
    }

    public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        => await Context.Set<User>()
            .Where(u => u.Id == userId)
            .Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfilePicture = u.ProfilePicture
            })
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<bool> IsUserInChatAsync(Guid chatId, Guid userId, CancellationToken cancellationToken)
        => await Context.Set<ChatUser>()
            .AnyAsync(c => c.ChatId == chatId && c.UserId == userId, cancellationToken);

    public async Task<bool> ExistsAsync(Guid chatId, CancellationToken cancellationToken)
        => await Context.Set<Chat>()
            .AnyAsync(c => c.Id == chatId, cancellationToken);

    public async Task<bool> OneToOneChatExistsAsync(Guid senderId, Guid receiverId, CancellationToken cancellationToken)
    {
        return await Context.Set<Chat>()
            .Where(c => c.ChatType == ChatType.Private.ToString())
            .AnyAsync(c => c.ChatUsers!.Any(cu => cu.UserId == senderId) &&
                           c.ChatUsers!.Any(cu => cu.UserId == receiverId), cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetChatUserIdsAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await Context.Set<ChatUser>()
            .Where(cu => cu.ChatId == chatId && cu.UserId != null)
            .Select(cu => cu.UserId!.Value)
            .ToListAsync(cancellationToken);
    }
}