namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class User
        {

            public static string CodePrefix { get; set; } = nameof(User);

            public static readonly ValidationItem FirstNameErr = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Name invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };

            public static readonly ValidationItem LastNameErr = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Last name invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };

            public static readonly ValidationItem EmailExists = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Email already exists in the database",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem EmailInvalidFormat = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = $"Email invalid format",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            
            public static readonly ValidationItem BirthDateInvalid = new ValidationItem
            {
                Code = $"{CodePrefix}5",
                Message = $"Birth date invalid",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem PasswError = new ValidationItem
            {
                Code = $"{CodePrefix}6",
                Message = $"Password not strong enough",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem UserDelete = new ValidationItem
            {
                Code = $"{CodePrefix}7",
                Message = $"Professor has active classes",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem UserEdit = new ValidationItem
            {
                Code = $"{CodePrefix}8",
                Message = $"Professor has active classes",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
