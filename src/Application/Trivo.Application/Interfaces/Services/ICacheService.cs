using Trivo.Application.Caching;

namespace Trivo.Application.Interfaces.Services;

/// <summary>
/// Cache-aside abstraction speaking in business terms — the caller owns how to fetch a value on a
/// miss, this only decides when to run that fetch. No knowledge of Redis leaks past this interface.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Returns the cached value for <paramref name="key"/>, or runs <paramref name="factory"/> on a
    /// miss, caches its result under <paramref name="options"/>, and returns it. A null result from
    /// the factory is never cached.
    /// </summary>
    Task<T?> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        CacheEntryOptions options,
        CancellationToken cancellationToken = default);

    /// <summary>Invalidates every key registered under any of the given tags.</summary>
    Task InvalidateByTagsAsync(IEnumerable<string> tags, CancellationToken cancellationToken = default);

    /// <summary>Invalidates a single, exactly-known key.</summary>
    Task InvalidateAsync(string key, CancellationToken cancellationToken = default);
}
