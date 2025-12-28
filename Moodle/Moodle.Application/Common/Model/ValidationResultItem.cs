using Moodle.Domain.Common.Validation;

namespace Moodle.Application.Common.Model
{
    public class ValidationResultItem
    {
        public ValidationSeverity Severity { get; init; }
        public ValidationType Type { get; init; }
        public string Code { get; init; }
        public string Message { get; init; }

        public static ValidationResultItem FromValidationItem(ValidationItem item)
        {
            return new ValidationResultItem
            {
                Severity = item.Severity,
                Type = item.Type,
                Code = item.Code,
                Message = item.Message
            };
        }
    }
}
