using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Moodle.Application.DTO;
using Moodle.Domain.Entities;
using Moodle.Infrastructure.Cache.Common;

namespace Moodle.Infrastructure.Cache
{
    public class UserCacheService : CacheService<UserDTO> , Domain.Services.Cache.IUserCacheService<UserDTO>
    {
        private readonly IDistributedCache _cache;
        public UserCacheService(IDistributedCache cache) : base(cache)
        {
            _cache = cache;
        }
        public async override Task<UserDTO?> GetAsync(string key, CancellationToken ct = default)
        {
            key = $"User_{key}";
            var value = await _cache.GetStringAsync(key, ct);
            return value is null ? null : JsonSerializer.Deserialize<UserDTO>(value);
        }
        public async override Task SetAsync(string key, UserDTO value, TimeSpan ttl, CancellationToken ct = default)
        {
            key = $"User_{key}";
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl
            };
            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, options, ct);
        }
    }
}
