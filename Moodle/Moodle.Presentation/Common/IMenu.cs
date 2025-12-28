using Moodle.Application.DTO;

public interface IMenu
{
    Task ShowAsync(UserDTO user);
}
