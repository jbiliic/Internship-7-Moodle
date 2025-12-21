using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence.Repository
{
    public interface IUserRepository : Common.IRepository<Entities.User, int>
    {
        Task<Entities.User?> GetByEmailAsync(string email);
        void SetAdmin (int userId);
        void SetProfessor (int userId);
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<IReadOnlyList<Conversation>?> GetConversationsAsync(int userId);
        Task<IReadOnlyList<Course>?> GetEnrolledInAsync(int userId);
        Task<IReadOnlyList<Course>?> GetManagedByAsync(int professorId);
    }
}
