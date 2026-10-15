using Pgvector;
using Trivo.Application.Interfaces.Repository.Base;
using Trivo.Domain.Enums;
using Trivo.Domain.Models;

namespace Trivo.Application.Interfaces.Repository.Account;

public interface IUserRepository : IGenericRepository<User>
{
    /// <summary>
    /// Verifies if a user's account is confirmed.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if the account is confirmed, false otherwise.</returns>
    Task<bool> IsAccountConfirmedAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a username is in use by a user other than the one specified.
    /// </summary>
    /// <param name="username">Username to verify.</param>
    /// <param name="userId">ID of the user to exclude from the verification.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if the username is in use, false otherwise.</returns>
    Task<bool> IsUsernameInUseAsync(string username, Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets the status of a user given their ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>User status or null if it does not exist.</returns>
    Task<string?> GetStatusAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for a user by their email.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The found user or null if they do not exist.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Filters and retrieves users who have at least one of the specified interests or skills.
    /// </summary>
    /// <param name="interestIds">Optional list of interest IDs to filter by.</param>
    /// <param name="skillIds">Optional list of skill IDs to filter by.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A collection of <see cref="User"/> objects matching the specified interests or skills.</returns>
    Task<IEnumerable<User>> GetByInterestsAndSkillsAsync(
        List<Guid>? interestIds,
        List<Guid>? skillIds,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user's skills given their ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A list of skill IDs belonging to the user.</returns>
    Task<List<Guid?>> GetSkillsAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user's interests given their ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A list of interest IDs belonging to the user.</returns>
    Task<List<Guid?>> GetInterestsAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if an email is in use, excluding a specific user.
    /// </summary>
    /// <param name="email">Email to verify.</param>
    /// <param name="excludeUserId">ID of the user to exclude.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task<bool> IsEmailInUseAsync(string email, Guid excludeUserId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a user's password.
    /// </summary>
    /// <param name="user">User whose password is being updated.</param>
    /// <param name="newHashedPassword">The new hashed password.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task UpdatePasswordAsync(User user, string newHashedPassword, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if an email exists in the database.
    /// </summary>
    /// <param name="email">Email to verify.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Verifies if a username exists in the database.
    /// </summary>
    /// <param name="username">Username to verify.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user with all their related information, such as interests and skills.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation if necessary.</param>
    /// <returns>A user with their full details.</returns>
    Task<User?> GetDetailsByIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a specific user along with their interests and skills.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A user with their interests or null if not found.</returns>
    Task<User?> GetUserWithInterestsAndSkillsAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all users including their interests and skills.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A collection of users with their interests and skills.</returns>
    Task<IEnumerable<User>> GetAllWithInterestsAndSkillsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets the list of interests associated with a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A collection of <see cref="UserInterest"/> objects.</returns>
    Task<IEnumerable<UserInterest>> GetInterestsByUserIdAsync(Guid userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets the list of skills associated with a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A collection of <see cref="UserSkill"/> objects.</returns>
    Task<IEnumerable<UserSkill>> GetSkillsByUserIdAsync(Guid userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets the role assigned to a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A string representing the user's role.</returns>
    Task<string> GetUserRoleAsync(Guid userId, CancellationToken cancellationToken);

    Task<User?> GetExpertAndRecruiterRelationshipsByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<User?> GetByIdWithRelationshipsAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a user's profile embedding vector and the hash of the text it was generated from.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="embedding">The new embedding vector.</param>
    /// <param name="profileTextHash">SHA-256 hash of the profile text the embedding was generated from.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task UpdateProfileEmbeddingAsync(Guid userId, Vector embedding, string profileTextHash, CancellationToken cancellationToken);

    /// <summary>
    /// Finds candidate users for recommendations by combining two signals in a single query:
    /// how many skills and interests they share with the requester (the structural overlap), and
    /// how close their profile embedding is (cosine distance). Candidates are restricted to active
    /// users of the target role, with a stored embedding and at least one skill or interest in
    /// common, excluding the requester and <paramref name="excludedUserIds"/>.
    /// </summary>
    /// <remarks>
    /// Results are ordered by overlap (highest first) and then by distance (closest first), and the
    /// pool is cut after that ordering. This matters: cutting by distance alone first would let the
    /// prose of a biography decide who even gets considered, so a candidate with exactly the right
    /// skills but a differently-worded profile could fall outside the pool and never be seen.
    /// The raw distance is returned alongside each candidate so the caller can apply its own
    /// relevance cutoff (e.g. cut at the largest gap between consecutive distances) — a fixed
    /// absolute threshold doesn't generalize, since "close" vs. "far" is relative to each query's
    /// own score distribution.
    /// </remarks>
    /// <param name="userId">User to exclude from the results.</param>
    /// <param name="embedding">The reference embedding to compare against.</param>
    /// <param name="targetRole">Role the candidates must belong to.</param>
    /// <param name="skillIds">The requester's skill ids; a candidate scores one point per skill shared.</param>
    /// <param name="interestIds">The requester's interest ids; a candidate scores one point per interest shared.</param>
    /// <param name="excludedUserIds">Users that must not be returned (e.g. existing matches). Applied in the database, before the pool is cut, so they never occupy a slot.</param>
    /// <param name="poolSize">
    /// How many candidates to fetch — larger than the final page size, so the caller has
    /// enough of the distance curve to detect a genuine relevance cutoff.
    /// </param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task<IReadOnlyList<(User User, double Distance)>> GetSimilarUsersAsync(
        Guid userId,
        Vector embedding,
        Roles targetRole,
        IReadOnlyCollection<Guid> skillIds,
        IReadOnlyCollection<Guid> interestIds,
        IReadOnlyCollection<Guid> excludedUserIds,
        int poolSize,
        CancellationToken cancellationToken);

    /// <summary>
    /// Free-text search over active users of the given role. Every term must match (case- and
    /// accent-insensitive "contains") at least one of: first name, last name, biography, position,
    /// company name (recruiters), or the name of one of the user's interests or skills. Paginated in
    /// the database. Existing matches are not filtered out — an explicit search shows everyone.
    /// </summary>
    /// <param name="requesterId">User performing the search; excluded from the results.</param>
    /// <param name="targetRole">Role the results must belong to (Expert or Recruiter).</param>
    /// <param name="terms">Search terms, already trimmed, de-duplicated and free of LIKE wildcards (%, _, \). Combined with AND.</param>
    /// <param name="pageNumber">1-based page number.</param>
    /// <param name="pageSize">Items per page.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The users on the requested page (with interests, skills, and role profile) and the total match count.</returns>
    Task<(IReadOnlyList<User> Items, int TotalItems)> SearchByTextAsync(
        Guid requesterId,
        Roles targetRole,
        IReadOnlyList<string> terms,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
