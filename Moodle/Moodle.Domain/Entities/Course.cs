using Moodle.Domain.Common.Entities;

namespace Moodle.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Major { get; set; }
        public int Semester { get; set; }
        public int ECTS { get; set; }
        public int ProfessorId { get; set; }

        public User Professor { get; set; } = null!;

    }
}
