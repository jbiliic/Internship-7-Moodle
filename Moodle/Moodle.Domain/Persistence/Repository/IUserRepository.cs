using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;

namespace Moodle.Domain.Persistence.Repository
{
    public interface IUserRepository : Common.IRepository<Entities.User, int>
    {
        Task<Entities.User?> GetByEmailAsync(string email);
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<IReadOnlyList<Course>?> GetEnrolledInAsync(int userId);
        Task<IReadOnlyList<Course>?> GetManagedByAsync(int professorId);
        Task<IReadOnlyList<User>> GetUsersWithConversation(int userId);
        Task<IReadOnlyList<User>> GetUsersWOConversation(int userId);
        Task<IReadOnlyList<User>> GetUsersEnrolledInByCourseIdAsync(int courseId);
        Task<IReadOnlyList<User>> GetAllStudentsAsync();
        Task<IReadOnlyList<User>> GetAllProfessorsAsync();
        Task<IReadOnlyList<User>> GetTop3MostMessagesAsync(DateTimeOffset time);
        Task<int> GetNumMessagesPerUser(int userId, DateTimeOffset date);
    }
}
