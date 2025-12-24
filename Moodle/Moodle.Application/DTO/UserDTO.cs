namespace Moodle.Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
