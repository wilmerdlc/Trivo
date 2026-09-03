using System.Text.Json;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Trivo.Application.Caching;
using Trivo.Application.Interfaces.Services;

namespace Trivo.Infrastructure.Persistence.Services;

public sealed class RedisCacheService(IConnectionMultiplexer redis, ILogger<RedisCacheService> logger) : ICacheService
{
    // Separates tag SETs ("tag:user:42") from data keys ("user:details:42") in the keyspace.
    private const string TagPrefix = "tag:";

    public async Task<T?> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        CacheEntryOptions options,
        CancellationToken cancellationToken = default)
    {
        var db = redis.GetDatabase();

        // Redis is best-effort: the database is the source of truth, so a Redis outage should
        // degrade to "always miss", never take the whole request down with it.
        try
        {
            var cached = await db.StringGetAsync(key);
            if (cached.HasValue)
            {
                logger.LogDebug("Cache HIT: {Key}", key);
                return JsonSerializer.Deserialize<T>(cached!);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Cache read failed for key {Key}; falling back to the factory.", key);
        }

        logger.LogDebug("Cache MISS: {Key}", key);

        var value = await factory();

        // Never cache nulls — a temporary "not found" shouldn't be remembered as permanent until
        // the TTL expires, e.g. hiding a record that gets created moments later.
        if (value is null)
        {
            return value;
        }

        try
        {
            await SetWithTagsAsync(db, key, value, options);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Cache write failed for key {Key}; returning the fetched value uncached.", key);
        }

        return value;
    }

    private static async Task SetWithTagsAsync<T>(IDatabase db, string key, T value, CacheEntryOptions options)
    {
        var serialized = JsonSerializer.Serialize(value);
        var expiry = options.AbsoluteExpiration;

        // One round trip for the data key plus every tag registration, instead of one per command.
        var batch = db.CreateBatch();
        var tasks = new List<Task> { batch.StringSetAsync(key, serialized, expiry) };

        foreach (var tag in options.Tags)
        {
            var tagKey = $"{TagPrefix}{tag}";
            tasks.Add(batch.SetAddAsync(tagKey, key));

            // The tag's SET must outlive the data key it tracks. If it expired at the same time
            // (or sooner), a key regenerated right after would register under a tag set that's
            // already gone — silently breaking future tag-based invalidation for that key.
            if (expiry.HasValue)
            {
                tasks.Add(batch.KeyExpireAsync(tagKey, expiry.Value.Add(TimeSpan.FromMinutes(1))));
            }
        }

        batch.Execute();
        await Task.WhenAll(tasks);
    }

    public async Task InvalidateByTagsAsync(IEnumerable<string> tags, CancellationToken cancellationToken = default)
    {
        var tagList = tags.ToList();
        if (tagList.Count == 0)
        {
            return;
        }

        // A failed invalidation must never fail the write that triggered it — the DB write already
        // succeeded independently of this. Worst case: the stale entry survives until its TTL expires.
        try
        {
            var db = redis.GetDatabase();

            var tagKeys = tagList.Select(t => (RedisKey)$"{TagPrefix}{t}").ToArray();
            var memberSets = await Task.WhenAll(tagKeys.Select(tk => db.SetMembersAsync(tk)));

            var dataKeys = memberSets
                .SelectMany(members => members)
                .Where(m => m.HasValue)
                .Select(m => (RedisKey)m.ToString())
                .Distinct()
                .ToArray();

            var batch = db.CreateBatch();
            var deleteTasks = new List<Task>();

            if (dataKeys.Length > 0)
            {
                deleteTasks.Add(batch.KeyDeleteAsync(dataKeys));
            }

            foreach (var tagKey in tagKeys)
            {
                deleteTasks.Add(batch.KeyDeleteAsync(tagKey));
            }

            batch.Execute();
            await Task.WhenAll(deleteTasks);

            logger.LogInformation("Invalidated {KeyCount} keys for tags: {Tags}", dataKeys.Length, string.Join(", ", tagList));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Cache invalidation failed for tags: {Tags}. Affected entries will persist until their TTL expires.", string.Join(", ", tagList));
        }
    }

    public async Task InvalidateAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var db = redis.GetDatabase();
            var deleted = await db.KeyDeleteAsync(key);
            logger.LogDebug("Invalidated key {Key}: {Deleted}", key, deleted);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Cache invalidation failed for key {Key}. The entry will persist until its TTL expires.", key);
        }
    }
}
