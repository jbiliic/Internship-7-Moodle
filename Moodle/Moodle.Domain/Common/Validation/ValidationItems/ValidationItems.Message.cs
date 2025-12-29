namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Message
        {
            public static string CodePrefix { get; set; } = nameof(Message);
            public static readonly ValidationItem EmptyContentErr = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Content cant be empty",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem ExceedMaxLengthErr = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Content exceeds maximum allowed length",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem EmptyTitleErr = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Title can not be empty",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
