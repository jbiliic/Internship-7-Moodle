using Moodle.Domain.Common.Entities;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;

namespace Moodle.Domain.Entities.Course
{
    public class CourseNotification : BaseEntity
    {
        public int CourseId { get; set; }
        public int ProfessorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Course Course { get; set; } = null!;
        public User Professor { get; set; } = null!;


        public ValidationResult Validate()
        {
            var Result = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Title))
            {
                Result.AddValidationItem(
                    ValidationItems.CourseNotifications.EmptyTitleErr
                    );
            }
            if (string.IsNullOrWhiteSpace(Content))
            {
                Result.AddValidationItem(
                    ValidationItems.CourseNotifications.EmptyContentErr
                    );
            }
            if (Content.Length + Title.Length > MessageMaxLen)
            {
                Result.AddValidationItem(
                    ValidationItems.CourseNotifications.ExceedMaxLengthErr
                    );
            }
            return Result;
        }
    }
}
