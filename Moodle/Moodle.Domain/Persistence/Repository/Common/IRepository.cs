namespace Moodle.Domain.Persistence.Repository.Common
{
    public interface IRepository<TEntity, Tvalue> where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity id);
        Task<TEntity?> GetByIdAsync(Tvalue id);
        Task<IReadOnlyList<TEntity>?> GetAllAsync();
    }
}
