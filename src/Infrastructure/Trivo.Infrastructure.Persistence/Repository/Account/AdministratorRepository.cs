using Microsoft.EntityFrameworkCore;
using Trivo.Application.Interfaces.Repository.Account;
using Trivo.Application.Pagination;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;
using Trivo.Infrastructure.Persistence.Base;
using Trivo.Infrastructure.Persistence.Context;

namespace Trivo.Infrastructure.Persistence.Repository.Account;

public class AdministratorRepository(TrivoContext context) :
    GenericRepository<Administrator>(context),
    IAdministratorRepository
{
    public async Task BanAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await Context.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user != null)
        {
            user.UserStatus = UserStatus.Banned.ToString();
            Context.Set<User>().Update(user);
        }
    }

    public async Task UnbanAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await Context.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user != null)
        {
            user.UserStatus = UserStatus.Active.ToString();
            Context.Set<User>().Update(user);
        }
    }

    public async Task<int> GetReportedCountAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Report>()
            .AsNoTracking()
            .Where(r => r.User != null)
            .Select(r => r.User!.Id)
            .Distinct()
            .CountAsync(cancellationToken);
    }

    public async Task<bool> IsUsernameInUseAsync(string username, Guid userId, CancellationToken cancellationToken) =>
        await ValidateAsync(u => u.Username == username && u.Id != userId, cancellationToken);

    public async Task<Administrator?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        await Context.Set<Administrator>()
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> IsEmailInUseAsync(string email, Guid excludeUserId, CancellationToken cancellationToken) =>
        await ValidateAsync(u => u.Email == email && u.Id != excludeUserId, cancellationToken);

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken) =>
        await ValidateAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken) =>
        await ValidateAsync(u => u.Username == username, cancellationToken);

    public async Task UpdatePasswordAsync(Administrator admin, string newPassword)
    {
        admin.PasswordHash = newPassword;
        Context.Set<Administrator>().Update(admin);
        await Task.CompletedTask;
    }

    public async Task<PagedResult<Report>> GetPagedLatestReportsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = Context.Set<Report>()
            .AsNoTracking()
            .Where(x => x.ReportStatus == ReportStatus.Pending.ToString())
            .Include(x => x.Message)
            .ThenInclude(m => m!.Sender)
            .Include(x => x.Message)
            .ThenInclude(m => m!.Receiver)
            .AsSplitQuery();

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Report>(items, total, pageNumber, pageSize);
    }

    public async Task<PagedResult<User>> GetPagedLatestUsersAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = Context.Set<User>()
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt);

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<User>(items, total, pageNumber, pageSize);
    }

    public async Task<IEnumerable<User>> GetLast10BannedUsersAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .AsNoTracking()
            .Where(x => x.UserStatus == UserStatus.Banned.ToString())
            .OrderByDescending(x => x.CreatedAt)
            .Take(10)
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<Match>> GetPagedLatestMatchesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = Context.Set<Match>()
            .AsNoTracking()
            .Include(m => m.Recruiter)
            .ThenInclude(r => r!.User)
            .Include(m => m.Expert)
            .ThenInclude(e => e!.User)
            .AsSplitQuery()
            .OrderByDescending(x => x.CreatedAt);

        var total = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Match>(items, total, pageNumber, pageSize);
    }

    public async Task<int> GetCountCompletedMatchesAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Match>()
            .AsNoTracking()
            .CountAsync(x => x.MatchStatus == MatchStatus.Completed.ToString(), cancellationToken);
    }

    public async Task<int> GetCountActiveUsersAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .AsNoTracking()
            .Where(x => x.UserStatus == UserStatus.Active.ToString())
            .CountAsync(cancellationToken);
    }
}