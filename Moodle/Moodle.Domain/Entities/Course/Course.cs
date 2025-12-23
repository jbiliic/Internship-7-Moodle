using Moodle.Domain.Common.Entities;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;

namespace Moodle.Domain.Entities.Course
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Major { get; set; }
        public int Semester { get; set; }
        public int ECTS { get; set; }
        public int ProfessorId { get; set; }

        public User Professor { get; set; } = null!;


        public ValidationResult ValidateBasic()
        {
            var res = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Name) || Name.Length > MaxNameLen)
            {
                res.AddValidationItem(
                    ValidationItems.Course.InvalidCourseName
                    );
            }
            if (string.IsNullOrWhiteSpace(Major) || Major.Length > MaxNameLen)
            {
                res.AddValidationItem(
                    ValidationItems.Course.MajorInvalid
                    );
            }
            if (Semester < 1 || Semester > 12)
            {
                res.AddValidationItem(
                    ValidationItems.Course.InvalidSemester
                    );
            }
            if (ECTS < 1 || ECTS > 60)
            {
                res.AddValidationItem(
                    ValidationItems.Course.InvalidECTS
                    );
            }
            return res;
        }
    }
}
