namespace Trivo.Application.Caching;

/// <summary>
/// TTL and tags for a single cache entry. <see cref="AbsoluteExpiration"/> is the safety net —
/// if a write forgets to invalidate a tag, the stale entry still dies on its own eventually.
/// </summary>
public sealed record CacheEntryOptions
{
    public TimeSpan? AbsoluteExpiration { get; init; }

    public IReadOnlyList<string> Tags { get; init; } = [];
}
