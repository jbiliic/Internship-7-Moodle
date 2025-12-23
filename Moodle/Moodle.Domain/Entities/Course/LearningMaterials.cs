using System.ComponentModel.DataAnnotations;
using Moodle.Domain.Common.Entities;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;
using ValidationResult = Moodle.Domain.Common.Validation.ValidationResult;

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

        Common.Validation.ValidationResult Validate()
        {
            var resault = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Title) || Title.Length > MaxNameLen)
            {
                resault.AddValidationItem(
                    ValidationItems.LearningMaterials.EmptyTitleErr
                    );
            }

            if (!Uri.TryCreate(FilePath, UriKind.Absolute, out var uri))
            {
                resault.AddValidationItem(
                    ValidationItems.LearningMaterials.InvalidURL
                    );
            }
            else if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                resault.AddValidationItem(
                    ValidationItems.LearningMaterials.InvalidURL
                    );
            }
            return resault;
        }
    }
}
