using Moodle.Application.DTO;
using Moodle.Application.Handlers.StudentCourse;

namespace Moodle.Presentation.Menus
{
    internal class StudentCourseMenu : IMenu
    {
        private readonly MenuRouter _router;
        private readonly GetEnrolledInHandler _getEnrolledInHandler;
        private readonly GetCourseNotifAndMatsHandler _getCourseNotifAndMatsHandler;
        public StudentCourseMenu(MenuRouter router , GetEnrolledInHandler getEnrolledInHandler , GetCourseNotifAndMatsHandler getCourseNotifAndMatsHandler)
        {
            _router = router;
            _getEnrolledInHandler = getEnrolledInHandler;
            _getCourseNotifAndMatsHandler = getCourseNotifAndMatsHandler;
        }
        public async Task ShowAsync(UserDTO currUser)
        {
            var res = await _getEnrolledInHandler.HandleGetEnrolledIn(currUser.Id);
            while (true)
            {
                Console.Clear();
                if (!res.Value.IsSuccess || res.Value.Items.Count == 0)
                {
                    Helper.Helper.clearDisplAndDisplMessage("You are not enrolled in any courses.");
                    return;
                }
                Console.WriteLine("Your Courses:");
                var number = 1;
                var input = -1;
                while (true)
                {
                    number = 1;
                    foreach (var course in res.Value.Items)
                    {
                        Console.WriteLine($"{number}. Name : {course.Name} ECTS : {course.ECTS} Semester : {course.Semester}");
                        number++;
                    }
                    input = Helper.Helper.getAndValidateInputInt("a course number or 0 to go back: ");
                    if (input == 0) return;
                    if (input>number || input<1)
                    {
                        Helper.Helper.clearDisplAndDisplMessage("Invalid input. Please try again.");
                    }
                    break;
                }
                var selectedCourse = res.Value.Items[input - 1];
                await CourseScreenAsync(currUser, selectedCourse);

            }
        }
        public async Task CourseScreenAsync(UserDTO currUser, CourseDTO course) {
            while (true) {
                Console.Clear();
                Console.WriteLine("1 - Course Notifications");
                Console.WriteLine("2 - Learning Materials");
                Console.WriteLine("0 - Back");
                Console.Write("Select an option: ");
                switch (Console.ReadKey().KeyChar) { 
                    case '1':
                        var notifs = await _getCourseNotifAndMatsHandler.HandleGetNotifsAsync(course.Id);
                        Helper.Helper.renderCourseNotifs(notifs.Value);
                        break;
                    case '2':
                        var mats = await _getCourseNotifAndMatsHandler.HandleGetMatsAsync(course.Id);
                        Helper.Helper.renderMats(mats.Value);
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
