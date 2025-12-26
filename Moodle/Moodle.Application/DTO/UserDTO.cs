using Moodle.Domain.Entities;

namespace Moodle.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool isProfessor { get; set; }
        public bool isAdministrator { get; set; }

        public UserDTO (User user) { 
            Id = user.Id;
            FirstName = user.FirstName;
            Email = user.Email;
            isProfessor = user.IsProfessor;
            isAdministrator = user.IsAdministrator;
        }
    }
}
