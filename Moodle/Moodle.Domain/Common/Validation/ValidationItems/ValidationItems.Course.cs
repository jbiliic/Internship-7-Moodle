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
                Message = $"Unos smijera nije validan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem InvalidCourseName = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Naziv kolegija nije validan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem InvalidSemester = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Unos semestra nije valjan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem InvalidECTS = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Unos ECTS bodova nije valjan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem CourseExists = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = $"Kolegij vec postoji na odabranom smijeru",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
