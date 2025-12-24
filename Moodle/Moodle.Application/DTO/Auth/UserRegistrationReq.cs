namespace Moodle.Application.DTO.Auth
{
    public class UserRegistrationReq
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTimeOffset? BirthDate { get; set; }
    }
}
