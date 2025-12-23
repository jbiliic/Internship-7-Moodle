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


        ValidationResult Validate()
        {
            var resault = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Title))
            {
                resault.AddValidationItem(
                    ValidationItems.CourseNotifications.EmptyTitleErr
                    );
            }
            if (string.IsNullOrWhiteSpace(Content))
            {
                resault.AddValidationItem(
                    ValidationItems.CourseNotifications.EmptyContentErr
                    );
            }
            if (Content.Length + Title.Length > MessageMaxLen)
            {
                resault.AddValidationItem(
                    ValidationItems.CourseNotifications.ExceedMaxLengthErr
                    );
            }
            return resault;
        }
    }
}
