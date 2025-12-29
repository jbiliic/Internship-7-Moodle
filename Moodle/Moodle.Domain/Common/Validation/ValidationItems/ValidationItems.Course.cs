namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Course
        {
            public static string CodePrefix { get; set; } = nameof(Course);
            public static readonly ValidationItem MajorInvalid = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Major is invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem InvalidCourseName = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Course name is invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem InvalidSemester = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Semester is invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem InvalidECTS = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"ECTS is invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem CourseExists = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = $"Course already exists in the selected major",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
