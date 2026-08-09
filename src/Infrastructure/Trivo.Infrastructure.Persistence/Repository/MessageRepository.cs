using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

using Trivo.Application.DTOs.Chat;

namespace Trivo.Infrastructure.Persistence.Repository;

/// <summary>
/// Repository responsible for message persistence operations.
/// </summary>
public class MessageRepository(TrivoContext context)
    : GenericRepository<Message>(context), IMessageRepository
{
    private readonly TrivoContext _context = context;

    public async Task<List<Message>> GetByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await _context.Set<Message>()
            .AsNoTracking()
            .Where(m => m.ChatId == chatId)
            .OrderBy(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Message?> GetLastByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await _context.Set<Message>()
            .AsNoTracking()
            .Where(m => m.ChatId == chatId)
            .OrderByDescending(m => m.SentAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Message?> GetLastWithContentByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await _context.Set<Message>()
            .AsNoTracking()
            .Where(m => m.ChatId == chatId && m.Content != null)
            .OrderByDescending(m => m.SentAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<MessageDto>> GetPagedByChatIdAsync(
        Guid chatId,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Message>()
            .AsNoTracking()
            .Where(m => m.ChatId == chatId)
            .OrderByDescending(m => m.SentAt);

        var total = await query.CountAsync(cancellationToken);

        var messages = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MessageDto(
                m.MessageId!.Value,
                m.ChatId!.Value,
                m.Content!,
                m.Status!,
                m.SentAt!.Value,
                m.SenderId!.Value,
                m.ReceiverId,
                m.Type ?? string.Empty
            ))
            .ToListAsync(cancellationToken);

        return new PagedResult<MessageDto>(messages, total, page, pageSize);
    }

    public async Task<Message?> GetWithUsersByIdAsync(Guid messageId, CancellationToken cancellationToken)
    {
        return await _context.Set<Message>()
            .AsNoTracking()
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .FirstOrDefaultAsync(m => m.MessageId == messageId, cancellationToken);
    }
}