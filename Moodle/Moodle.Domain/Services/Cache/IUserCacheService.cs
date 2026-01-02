namespace Moodle.Domain.Services.Cache
{
    public interface IUserCacheService<T> : Common.ICacheService<T> where T : class
    {
    }
}
