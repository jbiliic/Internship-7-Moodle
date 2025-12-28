namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class IsEnrolled
        {
            public static string CodePrefix { get; set; } = nameof(IsEnrolled);
            public static readonly ValidationItem StudentAlreadyEnrolled = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "The student is already enrolled in this course.",
                Severity = ValidationSeverity.Warning,
                Type = ValidationType.Business
            };
        }
    }
}
