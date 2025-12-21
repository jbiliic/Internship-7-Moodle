namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class CourseNotifications
        {
            public static string CodePrefix { get; set; } = nameof(CourseNotifications);
            public static readonly ValidationItem EmptyContentErr = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Sadrzaj poruke ne smije biti prazan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem ExceedMaxLengthErr = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Sadrzaj obavijesti prelazi maksimalnu dozvoljenu duzinu",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem EmptyTitleErr = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Naslov obavijesti ne smije biti prazan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
