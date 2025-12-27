using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;

namespace Moodle.Domain.Persistence.Repository
{
    public interface IUserRepository : Common.IRepository<Entities.User, int>
    {
        Task<Entities.User?> GetByEmailAsync(string email);
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<IReadOnlyList<Conversation>?> GetConversationsAsync(int userId);
        Task<IReadOnlyList<Course>?> GetEnrolledInAsync(int userId);
        Task<IReadOnlyList<Course>?> GetManagedByAsync(int professorId);
        Task<IReadOnlyList<User>> GetUsersWithConversation(int userId);
        Task<IReadOnlyList<User>> GetUsersWOConversation(int userId);
    }
}
