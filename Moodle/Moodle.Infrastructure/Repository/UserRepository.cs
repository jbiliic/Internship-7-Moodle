using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;
using Moodle.Domain.Persistence.Repository;
using Moodle.Infrastructure.Database;
using Moodle.Infrastructure.Repository.Common;

namespace Moodle.Infrastructure.Repository
{
    public class UserRepository : Repository<User, int>,IUserRepository
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
    }
}
