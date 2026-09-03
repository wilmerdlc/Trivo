using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository;

public interface IChatRepository : IGenericRepository<Chat>
{
    Task<IEnumerable<Chat>> Get10RecentChatsAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid chatId, CancellationToken cancellationToken);

    Task<PagedResult<Chat>> GetChatsByUserIdPagedAsync(Guid userId, int page, int pageSize,
        CancellationToken cancellationToken);

    Task<bool> IsUserInChatAsync(Guid chatId, Guid userId, CancellationToken cancellationToken);

    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether a one-to-one chat already exists between the two users.
    /// </summary>
    Task<bool> OneToOneChatExistsAsync(Guid senderId, Guid receiverId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the IDs of every user in a chat, for notification fan-out.
    /// </summary>
    Task<IReadOnlyList<Guid>> GetChatUserIdsAsync(Guid chatId, CancellationToken cancellationToken);
}