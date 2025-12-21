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
                Message = $"Unos imena nije valjan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };

            public static readonly ValidationItem LastNameErr = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Unos prezimena nije valjan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };

            public static readonly ValidationItem EmailExists = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Email vec postoji u bazi podataka",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem EmailInvalidFormat = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = $"Email nije validnog formata",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            
            public static readonly ValidationItem BirthDateInvalid = new ValidationItem
            {
                Code = $"{CodePrefix}5",
                Message = $"Uneseni datum rodenja nije validan",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
            public static readonly ValidationItem PasswError = new ValidationItem
            {
                Code = $"{CodePrefix}6",
                Message = $"Lozinka nije dovoljno cvrsta",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.Formal
            };
        }
    }
}
