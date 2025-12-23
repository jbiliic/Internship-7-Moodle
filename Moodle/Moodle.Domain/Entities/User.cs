using System.Text.RegularExpressions;
using Moodle.Domain.Common.Entities;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;

namespace Moodle.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public bool IsProfessor { get; set; } = false;
        public bool IsAdministrator { get; set; } = false;

        public ValidationResult ValidateBasic()
        {
            var res = new ValidationResult();

            if(!string.IsNullOrWhiteSpace(FirstName))
                if ( FirstName.Length < 2 || FirstName.Length > MaxNameLen) 
                    res.AddValidationItem(
                        ValidationItems.User.FirstNameErr
                        );
            if(!string.IsNullOrWhiteSpace(LastName))
                    if (LastName.Length < 2 || LastName.Length > MaxNameLen)
                    res.AddValidationItem(
                        ValidationItems.User.LastNameErr
                        );
            if (!Regex.IsMatch(Email, RegexMailPattern))
                res.AddValidationItem(
                    ValidationItems.User.EmailInvalidFormat
                );
            if (DateOfBirth.HasValue)
            {
                var today = DateTimeOffset.UtcNow;
                if (DateOfBirth.Value > today || (today.Year - DateOfBirth.Value.Year) > 120)
                {
                    res.AddValidationItem(
                        ValidationItems.User.BirthDateInvalid
                    );
                }
            }
            return res;
        }
    }
}
