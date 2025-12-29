using Moodle.Domain.Entities;
using Moodle.Domain.Entities.Course;
using Moodle.Domain.Persistence.Repository.Common;

namespace Moodle.Domain.Persistence.Repository
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<IReadOnlyList<CourseNotification>?> GetNotifsByCourseAsync(int courseId);
        Task<IReadOnlyList<LearningMaterials>?> GetMaterialsByCourseAsync(int courseId);
        Task<Course> GetByNameAndMajorAsync(string name, string major);
        Task<User?> GetProfessor(int courseId);
        Task<IReadOnlyList<Course>?> GetCoursesManagedByProfessor(int professorId);
        Task<bool> IsStudentEnrolledAsync(int userId, int courseId);
    }
}
