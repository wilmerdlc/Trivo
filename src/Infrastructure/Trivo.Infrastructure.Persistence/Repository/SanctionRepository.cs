using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository;

public class SanctionRepository(TrivoContext context) : GenericRepository<Sanction>(context), ISanctionRepository
{
    public async Task<Sanction?> GetCurrentBlockAsync(
        Guid userId,
        DateTime nowUtc,
        CancellationToken cancellationToken)
    {
        var warning = SanctionType.Warning.ToString();
        var permanent = SanctionType.PermanentBan.ToString();

        // A permanent ban outranks any suspension; otherwise the one that ends last wins.
        return await Context.Set<Sanction>()
            .AsNoTracking()
            .Where(s => s.UserId == userId &&
                        s.Type != warning &&
                        s.RevokedAt == null &&
                        (s.ExpiresAt == null || s.ExpiresAt > nowUtc))
            .OrderByDescending(s => s.Type == permanent)
            .ThenByDescending(s => s.ExpiresAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task RevokeActiveAsync(Guid userId, DateTime nowUtc, CancellationToken cancellationToken)
    {
        var warning = SanctionType.Warning.ToString();

        var active = await Context.Set<Sanction>()
            .Where(s => s.UserId == userId &&
                        s.Type != warning &&
                        s.RevokedAt == null &&
                        (s.ExpiresAt == null || s.ExpiresAt > nowUtc))
            .ToListAsync(cancellationToken);

        foreach (var sanction in active)
        {
            sanction.RevokedAt = nowUtc;
            sanction.UpdatedAt = nowUtc;
        }
    }

    public async Task<PagedResult<Sanction>> GetPagedHistoryAsync(
        Guid? userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = Context.Set<Sanction>().AsNoTracking().AsQueryable();

        if (userId is not null)
        {
            query = query.Where(s => s.UserId == userId);
        }

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(s => s.CreatedAt)
            .ThenBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(s => s.User)
            .Include(s => s.Admin)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        return new PagedResult<Sanction>(items, total, pageNumber, pageSize);
    }
}
