using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;
using Moodle.Domain.Persistence.Repository;
using Moodle.Infrastructure.Database;
using Moodle.Infrastructure.Repository.Common;

namespace Moodle.Infrastructure.Repository
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        private readonly MoodleDbContext _context;
        public CourseRepository(MoodleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Course> GetByNameAndMajorAsync(string name, string major)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Name == name && c.Major == major);
        }

        public async Task<IReadOnlyList<LearningMaterials>?> GetMaterialsByCourseAsync(int courseId)
        {
            return await _context.LearningMaterials
                .Where(lm => lm.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<CourseNotification>?> GetNotifsByCourseAsync(int courseId)
        {
            return await _context.CourseNotifications
                .Where(cn => cn.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<User?> GetProfessor(int courseId)
        {
            return await _context.Courses
                .Where(c => c.Id == courseId)
                .Select(c => c.Professor)
                .FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<Course>?> GetCoursesManagedByProfessor(int professorId)
        {
            return await _context.Courses
                .Where(c => c.ProfessorId == professorId)
                .ToListAsync();
        }
    }
}
