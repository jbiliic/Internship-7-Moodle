namespace Moodle.Application.DTO.Auth
{
    public class UserLoginReq
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
