using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Application.Interfaces.Repository;

/// <summary>
/// Defines persistence operations for chat messages.
/// </summary>
public interface IMessageRepository : IGenericRepository<Message>
{
    /// <summary>
    /// Retrieves all messages belonging to a chat ordered by sent date ascending.
    /// </summary>
    Task<List<Message>> GetByChatIdAsync(Guid chatId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the last message sent in a chat.
    /// </summary>
    Task<Message?> GetLastByChatIdAsync(Guid chatId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the last message with content in a chat.
    /// </summary>
    Task<Message?> GetLastWithContentByChatIdAsync(Guid chatId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves paginated messages for a chat.
    /// </summary>
    Task<PagedResult<MessageDto>> GetPagedByChatIdAsync(
        Guid chatId,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a message including sender and receiver.
    /// </summary>
    Task<Message?> GetWithUsersByIdAsync(Guid messageId, CancellationToken cancellationToken);
}