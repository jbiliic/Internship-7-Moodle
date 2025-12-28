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

        public Common.Validation.ValidationResult Validate()
        {
            var Result = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Title) || Title.Length > MaxNameLen)
            {
                Result.AddValidationItem(
                    ValidationItems.LearningMaterials.EmptyTitleErr
                    );
            }

            if (!Uri.TryCreate(FilePath, UriKind.Absolute, out var uri))
            {
                Result.AddValidationItem(
                    ValidationItems.LearningMaterials.InvalidURL
                    );
            }
            else if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                Result.AddValidationItem(
                    ValidationItems.LearningMaterials.InvalidURL
                    );
            }
            return Result;
        }
    }
}
