using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Application.Pagination;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository;

public interface IReportRepository : IGenericRepository<Report>
{
    /// <summary>
    /// Gets a report with everything an administrator needs to review it: both users (with their
    /// Expert/Recruiter profile), the reported message, the reviewing admin and the resulting
    /// sanction. Read-only — use <c>GetByIdAsync</c> when the report is going to be modified.
    /// </summary>
    Task<Report?> GetDetailsByIdAsync(Guid reportId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a page of reports, newest first, optionally filtered by status.
    /// </summary>
    /// <param name="status">A <see cref="Domain.Enums.ReportStatus"/> name, or null for all.</param>
    Task<PagedResult<Report>> GetPagedForAdminAsync(
        string? status,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets the most recent reports, newest first.
    /// </summary>
    Task<IReadOnlyList<Report>> GetLatestAsync(int count, CancellationToken cancellationToken);

    /// <summary>
    /// Whether the reporter already has a pending report for the same target: the same message
    /// when <paramref name="messageId"/> is set, otherwise the same reported user's profile.
    /// </summary>
    Task<bool> HasPendingReportAsync(
        Guid reporterId,
        Guid reportedUserId,
        Guid? messageId,
        CancellationToken cancellationToken);
}
