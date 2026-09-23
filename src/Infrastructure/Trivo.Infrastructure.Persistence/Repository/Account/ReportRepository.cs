using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository;
using Trivo.Application.Pagination;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository.Account;

public class ReportRepository(TrivoContext context) : GenericRepository<Report>(context), IReportRepository
{
    public async Task<Report?> GetDetailsByIdAsync(Guid reportId, CancellationToken cancellationToken) =>
        await Context.Set<Report>()
            .AsNoTracking()
            .Include(r => r.Reporter)!.ThenInclude(u => u!.Experts)
            .Include(r => r.Reporter)!.ThenInclude(u => u!.Recruiters)
            .Include(r => r.ReportedUser)!.ThenInclude(u => u!.Experts)
            .Include(r => r.ReportedUser)!.ThenInclude(u => u!.Recruiters)
            .Include(r => r.Message)
            .Include(r => r.ReviewedByAdmin)
            .Include(r => r.Sanction)
            .AsSplitQuery()
            .FirstOrDefaultAsync(r => r.ReportId == reportId, cancellationToken);

    public async Task<PagedResult<Report>> GetPagedForAdminAsync(
        string? status,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = Context.Set<Report>().AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(r => r.ReportStatus == status);
        }

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(r => r.CreatedAt)
            .ThenBy(r => r.ReportId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.Reporter)
            .Include(r => r.ReportedUser)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

        return new PagedResult<Report>(items, total, pageNumber, pageSize);
    }

    public async Task<IReadOnlyList<Report>> GetLatestAsync(int count, CancellationToken cancellationToken) =>
        await Context.Set<Report>()
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .ThenBy(r => r.ReportId)
            .Take(count)
            .Include(r => r.Reporter)
            .Include(r => r.ReportedUser)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);

    public async Task<bool> HasPendingReportAsync(
        Guid reporterId,
        Guid reportedUserId,
        Guid? messageId,
        CancellationToken cancellationToken)
    {
        var pending = ReportStatus.Pending.ToString();

        return messageId is null
            ? await ValidateAsync(
                r => r.ReportedById == reporterId &&
                     r.ReportedUserId == reportedUserId &&
                     r.MessageId == null &&
                     r.ReportStatus == pending,
                cancellationToken)
            : await ValidateAsync(
                r => r.ReportedById == reporterId &&
                     r.MessageId == messageId &&
                     r.ReportStatus == pending,
                cancellationToken);
    }
}
