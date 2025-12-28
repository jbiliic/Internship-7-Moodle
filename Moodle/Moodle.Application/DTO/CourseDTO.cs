using Moodle.Domain.Entities.Course;

namespace Moodle.Application.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = string.Empty;
        public int Semester { get; set; }
        public int ECTS { get; set; }
        public CourseDTO(string name, int semester, int ects)
        {
            Name = name;
            Semester = semester;
            ECTS = ects;
        }
        public CourseDTO(Course course) {
            Id = course.Id;
            Name = course.Name;
            Semester = course.Semester;
            ECTS = course.ECTS;
        }
    }
}
