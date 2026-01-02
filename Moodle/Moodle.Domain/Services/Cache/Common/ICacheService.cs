namespace Moodle.Domain.Services.Cache.Common
{
    public interface ICacheService<T> where T : class
    {
        Task<T?> GetAsync(string key, CancellationToken ct = default);
        Task SetAsync(string key, T value, TimeSpan ttl, CancellationToken ct = default);
    }
}
