using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence.Repository
{
    public interface ICourseRepository
    {
        Task<IReadOnlyList<CourseNotification>?> GetNotifsByCourseAsync(int courseId);
        Task<IReadOnlyList<LearningMaterials>?> GetMaterialsByCourseAsync(int courseId);
        Task<Course> GetByNameAndMajorAsync(string name, string major);
    }
}
