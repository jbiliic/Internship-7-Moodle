using System.Runtime.CompilerServices;
using Moodle.Application.DTO;
using Moodle.Application.Handlers;
using Moodle.Application.Handlers.StudentCourse;

namespace Moodle.Presentation.Menus
{
    internal class ProfessorCourseMenu : IMenu
    {
        private readonly GetProfessorCoursesHandler _getProfessorCoursesHandler;
        private readonly GetUsersEnrolledInHandler _getUsersEnrolledInHandler;
        private readonly GetCourseNotifAndMatsHandler _getCourseNotifAndMatsHandler;
        public ProfessorCourseMenu(GetProfessorCoursesHandler getProfessorCoursesHandler , 
            GetUsersEnrolledInHandler getUsersEnrolledInHandler , 
            GetCourseNotifAndMatsHandler getCourseNotifAndMatsHandler)
        {
            _getProfessorCoursesHandler = getProfessorCoursesHandler;
            _getUsersEnrolledInHandler = getUsersEnrolledInHandler;
            _getCourseNotifAndMatsHandler = getCourseNotifAndMatsHandler;
        }
        public async Task ShowAsync(UserDTO currUser)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Professor Course Menu");
                var res = await _getProfessorCoursesHandler.HandleGetManagedByProf(currUser.Id);
                if (!res.Value.IsSuccess || res.Value.Items.Count == 0)
                {
                    Helper.Helper.clearDisplAndDisplMessage("So so empty...");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Your Courses:");
                var number = 1;
                var input = -1;
                foreach (var course in res.Value.Items)
                {
                    Console.WriteLine($"{number++}. {course.Name}");
                }
                input = Helper.Helper.getAndValidateInputInt(" a course or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > res.Value.Items.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }
                var selectedCourse = res.Value.Items[input - 1];
                await ViewCourseAsync(currUser, selectedCourse);
            }

        }

        public async Task ViewCourseAsync(UserDTO currUser, CourseDTO course)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Students");
                Console.WriteLine("2. Notifications");
                Console.WriteLine("3. Materials");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var userRes = await _getUsersEnrolledInHandler.HandleGetUsersAsync(course.Id);
                        Helper.Helper.renderSortedUsers(userRes.Value);
                        break;
                    case '2':
                        var notifRes = await _getCourseNotifAndMatsHandler.HandleGetNotifsAsync(course.Id);
                        Helper.Helper.renderCourseNotifs(notifRes.Value);
                        break;
                    case '3':
                        var matsRes = await _getCourseNotifAndMatsHandler.HandleGetMatsAsync(course.Id);
                        Helper.Helper.renderMats(matsRes.Value);
                        break;
                    case '0':
                        return;
                    default:
                        Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
