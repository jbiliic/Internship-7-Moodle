using Moodle.Domain.Common.Entities;

namespace Moodle.Domain.Entities
{
    public class CourseNotification : BaseEntity
    {
        public int CourseId { get; set; }
        public int ProfessorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Course Course { get; set; } = null!;
        public User Professor { get; set; } = null!;
    }
}
