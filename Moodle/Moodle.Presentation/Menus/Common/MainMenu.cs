using Moodle.Application.DTO;
using Moodle.Presentation.Menus.Professor;
using Moodle.Presentation.Menus.Student;

namespace Moodle.Presentation.Menus.Common
{
    internal class MainMenu : IMenu
    {
        private readonly MenuRouter _router;
        public MainMenu(MenuRouter router)
        {
            _router = router;
        }
        public static Dictionary<char, string> studentMenuOptions = new Dictionary<char, string>
        {
            { '1', "My Courses" },
            { '2', "Chat" },
            { '0', "Logout" }
        };
        public static Dictionary<char, string> professorMenuOptions = new Dictionary<char, string>
        {
            { '1', "My Courses" },
            { '2', "Chat" },
            { '3', "Manage Courses" },
            { '0', "Logout" }
        };
        public static Dictionary<char, string> adminMenuOptions { get; } = new Dictionary<char, string>
        {
            { '1', "Manage Users" },
            { '2', "Chat" },
            { '0', "Logout" }
        };

       

        public  async Task ShowAsync(UserDTO currUser) {
            var options = new Dictionary<char, string>();
            if (currUser.isAdministrator) {
                options = adminMenuOptions;
            }
            else if (currUser.isProfessor) {
                options = professorMenuOptions;
            }
            else {
                options = studentMenuOptions;
            }

            while (true)
            {
                Console.Clear();

                foreach (var option in options)
                {
                    Console.WriteLine($"{option.Key}. {option.Value}");
                }
                Console.Write("Select an option: ");

                var input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (input == '0')
                    return;

                if (currUser.isAdministrator)
                {
                    switch (input)
                    {
                        case '1':
                            //await _router.NavigateTo<ManageUsersMenu>(currUser);
                            break;

                        case '2':
                            await _router.NavigateTo<ChatMenu>(currUser);
                            break;

                        default:
                            Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                            break;
                    }
                }
                else if (currUser.isProfessor)
                {
                    switch (input)
                    {
                        case '1':
                            await _router.NavigateTo<ProfessorCourseMenu>(currUser);
                            break;

                        case '2':
                            await _router.NavigateTo<ChatMenu>(currUser);
                            break;

                        case '3':
                            await _router.NavigateTo<ManageCourseMenu>(currUser);
                            break;

                        default:
                            Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                            break;
                    }
                }
                else 
                {
                    switch (input)
                    {
                        case '1':
                            await _router.NavigateTo<StudentCourseMenu>(currUser);
                            break;

                        case '2':
                            await _router.NavigateTo<ChatMenu>(currUser);
                            break;

                        default:
                            Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                            break;
                    }
                }
            }
        }
    }

}

