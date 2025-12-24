namespace Moodle.Application.Common
{
    public interface IMoodleDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
