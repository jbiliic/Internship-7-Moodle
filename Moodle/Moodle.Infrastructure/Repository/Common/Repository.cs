using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Persistence.Repository.Common;

namespace Moodle.Infrastructure.Repository.Common
{
    public class Repository<TEntity, Tvalue> : IRepository<TEntity, Tvalue> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public void Delete(TEntity id)
        {
            _dbSet.Remove(id);
        }

        public async Task<IReadOnlyList<TEntity>?> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Tvalue id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
             await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
