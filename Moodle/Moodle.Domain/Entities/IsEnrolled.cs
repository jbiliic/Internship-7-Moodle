using Moodle.Domain.Common.Entities;
using Moodle.Domain.Entities.Course;
namespace Moodle.Domain.Entities
{
    public class IsEnrolled : BaseEntity
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public User User { get; set; } = null!;
        public Moodle.Domain.Entities.Course.Course Course { get; set; } = null!;
    }
}
