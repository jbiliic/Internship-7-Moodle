using Moodle.Domain.Common.Entities;

namespace Moodle.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public bool IsProfessor { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
