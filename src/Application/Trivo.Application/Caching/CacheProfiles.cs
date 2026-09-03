namespace Trivo.Application.Caching;

/// <summary>
/// TTLs grouped by how often the underlying data actually changes, not by entity — callers pick
/// a volatility bucket and add their own tags with <c>CacheProfiles.Warm with { Tags = [...] }</c>.
/// </summary>
public static class CacheProfiles
{
    /// <summary>Rarely changes: catalogs, categories, reference data.</summary>
    public static CacheEntryOptions Cold => new()
    {
        AbsoluteExpiration = TimeSpan.FromHours(6)
    };

    /// <summary>Normal business data: user profiles, skills, interests.</summary>
    public static CacheEntryOptions Warm => new()
    {
        AbsoluteExpiration = TimeSpan.FromMinutes(30)
    };

    /// <summary>Changes often or is read where staleness is visible: admin dashboards.</summary>
    public static CacheEntryOptions Hot => new()
    {
        AbsoluteExpiration = TimeSpan.FromMinutes(5)
    };
}
