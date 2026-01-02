using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Moodle.Domain.Services.Cache.Common;

namespace Moodle.Infrastructure.Cache.Common
{
    public class CacheService<T> : ICacheService<T> where T : class
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async virtual Task<T?> GetAsync(string key, CancellationToken ct = default)
        {
            var value = await _cache.GetStringAsync(key, ct);
            return value is null ? null : JsonSerializer.Deserialize<T>(value);
        }

        public async virtual Task SetAsync(string key, T value, TimeSpan ttl, CancellationToken ct = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl
            };
            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, options, ct);
        }
    }
}
