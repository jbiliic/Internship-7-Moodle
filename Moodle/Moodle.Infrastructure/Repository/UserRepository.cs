using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;
using Moodle.Domain.Persistence.Repository;
using Moodle.Infrastructure.Database;
using Moodle.Infrastructure.Repository.Common;

namespace Moodle.Infrastructure.Repository
{
    public class UserRepository : Repository<User, int> , IUserRepository
    {
        private readonly MoodleDbContext _context;
        public UserRepository(MoodleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<IReadOnlyList<User>> GetAllStudentsAsync()
        {
            return await _context.Users
                .Where(u => !u.IsProfessor && !u.IsAdministrator)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IReadOnlyList<Conversation>?> GetConversationsAsync(int userId)
        {
            return await _context.Conversations
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Course>?> GetEnrolledInAsync(int userId)
        {
            return await _context.Enrollments
                .Where(e => e.UserId == userId)
                .Select(e => e.Course)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Course>?> GetManagedByAsync(int professorId)
        {
            return await _context.Courses
                .Where(c => c.ProfessorId == professorId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetUsersEnrolledInByCourseIdAsync(int courseId)
        {
            return await _context.Users
                .Where(u => _context.Enrollments
                    .Any(e => e.CourseId == courseId && e.UserId == u.Id))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetUsersWithConversation(int userId)
        {
            return await _context.Conversations
                .Where(c => 
                    (c.User1Id == userId || c.User2Id == userId))
                .Select(c => c.User1Id == userId
                    ? c.User2
                    : c.User1)
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetUsersWOConversation(int userId)
        {
            return await _context.Users
                .Where(u => u.Id != userId &&
                    !_context.Conversations.Any(c =>
                        (c.User1Id == userId && c.User2Id == u.Id) ||
                        (c.User2Id == userId && c.User1Id == u.Id)))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
