using Moodle.Domain.Common.Entities;
using Moodle.Domain.Common.Validation;

namespace Moodle.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public bool IsProfessor { get; set; } = false;
        public bool IsAdministrator { get; set; } = false;

        public ValidationResult ValidateBasic()
        {
            var res = new ValidationResult();


        }
    }
}
