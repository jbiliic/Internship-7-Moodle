using Moodle.Domain.Common.Entities;

namespace Moodle.Domain.Entities.Course
{
    public class LearningMaterials : BaseEntity
    {
        public int CourseId { get; set; }
        public int UploaderId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }

        public Course Course { get; set; } = null!;
        public User Uploader { get; set; } = null!;
    }
}
