using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository;

public interface ISanctionRepository : IGenericRepository<Sanction>
{
    /// <summary>
    /// Gets the sanction currently restricting the user's account, if any: a permanent ban, or
    /// the temporary suspension that ends last. Warnings and revoked/expired sanctions are ignored.
    /// </summary>
    Task<Sanction?> GetCurrentBlockAsync(Guid userId, DateTime nowUtc, CancellationToken cancellationToken);

    /// <summary>
    /// Marks every active restricting sanction of the user as revoked. Doesn't save.
    /// </summary>
    Task RevokeActiveAsync(Guid userId, DateTime nowUtc, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a page of the sanction history, newest first, optionally for a single user.
    /// </summary>
    Task<PagedResult<Sanction>> GetPagedHistoryAsync(
        Guid? userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
