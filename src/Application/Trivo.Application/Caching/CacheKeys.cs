namespace Trivo.Application.Caching;

/// <summary>
/// Every cache key and tag used by the app, centralized so a format change happens in one place.
/// </summary>
public static class CacheKeys
{
    // Users
    public static string UserBiography(Guid userId) => $"user:biography:{userId}";
    public static string UserSkills(Guid userId) => $"user:skills:{userId}";
    public static string UserInterests(Guid userId) => $"user:interests:{userId}";
    public static string UserProfilePicture(Guid userId) => $"user:profile-picture:{userId}";
    public static string UserDetails(Guid userId) => $"user:details:{userId}";
    public static string UserDetailsExpert(Guid userId) => $"user:details:expert:{userId}";
    public static string UserDetailsRecruiter(Guid userId) => $"user:details:recruiter:{userId}";

    // Skills
    public static string SkillSearch(string name) => $"skill:search:{name.Trim().ToLowerInvariant()}";
    public static string SkillsPaged(int pageNumber, int pageSize) => $"skill:paged:{pageNumber}:{pageSize}";

    // Interests
    public static string InterestSearch(string name) => $"interest:search:{name.Trim().ToLowerInvariant()}";
    public static string InterestsPaged(int pageNumber, int pageSize) => $"interest:paged:{pageNumber}:{pageSize}";

    public static string InterestsByCategory(IEnumerable<Guid> categoryIds, int pageNumber, int pageSize) =>
        $"interest:by-category:{string.Join('-', categoryIds)}:{pageNumber}:{pageSize}";

    // Interest categories
    public static string InterestCategoriesPaged(int pageNumber, int pageSize) =>
        $"interest-category:paged:{pageNumber}:{pageSize}";

    // Admin
    public static string AdminLatestUsers(int pageNumber, int pageSize) => $"admin:latest-users:{pageNumber}:{pageSize}";
    public static string AdminLatestMatches(int pageNumber, int pageSize) => $"admin:latest-matches:{pageNumber}:{pageSize}";
    public const string AdminLastBannedUsers = "admin:last-banned-users";

    // Tags — group keys that must be invalidated together.
    public static string UserTag(Guid userId) => $"user:{userId}";
    public const string SkillCatalogTag = "catalog:skills";
    public const string InterestCatalogTag = "catalog:interests";
    public const string InterestCategoryCatalogTag = "catalog:interest-categories";
    public const string AdminUsersTag = "admin:users";
    public const string AdminMatchesTag = "admin:matches";
}
