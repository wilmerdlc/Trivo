using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository.Account;

public interface IRecruiterRepository : IGenericRepository<Recruiter>
{
    /// <summary>
    /// Filters a list of recruiters based on specific skills and interests.
    /// </summary>
    /// <param name="skillIds">List of skill IDs to filter.</param>
    /// <param name="interestIds">List of interest IDs to filter.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation if necessary.</param>
    /// <returns>A task representing the asynchronous operation. The result contains a list of recruiters who meet the filters.</returns>
    Task<IEnumerable<Recruiter>> GetBySkillsAndInterestsAsync(
        List<Guid> skillIds,
        List<Guid> interestIds,
        CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if the user is registered as a recruiter.
    /// </summary>
    Task<bool> IsUserRecruiterAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the details of a recruiter by user ID.
    /// </summary>
    Task<Recruiter?> GetDetailsAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the details of every recruiter whose user ID is in the given list, in one query.
    /// </summary>
    Task<IReadOnlyList<Recruiter>> GetDetailsByUserIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the recruiter record associated with a specific user ID.
    /// </summary>
    Task<Recruiter?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}