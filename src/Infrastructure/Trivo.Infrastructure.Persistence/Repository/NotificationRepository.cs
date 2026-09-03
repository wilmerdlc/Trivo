using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public sealed class NotificationRepository(TrivoContext context)
    : GenericRepository<Notification>(context), INotificationRepository
{
    /// <summary>
    /// Retrieves paginated notifications belonging to a specific user.
    /// </summary>
    public async Task<PagedResult<Notification>> GetPagedByUserIdAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        // No Include(n => n.User) — NotificationDto only ever reads the UserId scalar, never
        // the User navigation, so loading it was pure waste on every page of notifications.
        var query = Context.Set<Notification>()
            .AsNoTracking()
            .Where(n => n.UserId == userId);

        var totalCount = await query.CountAsync(cancellationToken);

        var notifications = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Notification>(
            notifications,
            totalCount,
            pageNumber,
            pageSize);
    }

    /// <summary>
    /// Retrieves a notification by ID ensuring it belongs to the specified user.
    /// </summary>
    public async Task<Notification?> GetByIdAndUserIdAsync(
        Guid notificationId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<Notification>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                n => n.NotificationId == notificationId &&
                     n.UserId == userId,
                cancellationToken);
    }
}