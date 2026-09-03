using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository.Account;

public interface IAdministratorRepository : IGenericRepository<Administrator>
{
    Task BanAsync(Guid userId, CancellationToken cancellationToken);

    Task UnbanAsync(Guid userId, CancellationToken cancellationToken);

    Task<PagedResult<Report>> GetPagedLatestReportsAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetLast10BannedUsersAsync(CancellationToken cancellationToken);

    Task<bool> IsUsernameInUseAsync(string username, Guid userId, CancellationToken cancellationToken);

    Task<Administrator?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<bool> IsEmailInUseAsync(string email, Guid excludeUserId, CancellationToken cancellationToken);

    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);

    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);

    Task UpdatePasswordAsync(Administrator admin, string newPassword);

    Task<PagedResult<User>> GetPagedLatestUsersAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<PagedResult<Match>> GetPagedLatestMatchesAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    Task<int> GetCountCompletedMatchesAsync(CancellationToken cancellationToken);

    Task<int> GetCountActiveUsersAsync(CancellationToken cancellationToken);

    Task<int> GetReportedCountAsync(CancellationToken cancellationToken);
}