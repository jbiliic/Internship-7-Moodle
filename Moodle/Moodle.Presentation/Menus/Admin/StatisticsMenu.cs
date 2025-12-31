using Moodle.Application.DTO;
using Moodle.Application.Handlers.Admin;
using Moodle.Application.Handlers.Professor;
using Moodle.Application.Handlers.Stats;

namespace Moodle.Presentation.Menus.Admin
{
    internal class StatisticsMenu : IMenu
    {
        private readonly GetAllUsersHandler _getAllUsersHandler;
        private readonly GetAllCoursesHandler _getAllCoursesHandler;
        private readonly GetCoursesPerEnrollHandler _getCoursesPerEnrollHandler;
        private readonly GetUsersEnrolledInHandler _getUsersEnrolledInHandler;
        private readonly GetUsersPerNumMessage _getUsersPerNumMessageHandler;
        public StatisticsMenu(GetAllUsersHandler getAllUsersHandler,
            GetAllCoursesHandler getAllCoursesHandler,
            GetCoursesPerEnrollHandler getCoursesPerEnrollHandler,
            GetUsersEnrolledInHandler getUsersEnrolledInHandler,
            GetUsersPerNumMessage getUsersPerNumMessageHandler)
        {
            this._getAllUsersHandler = getAllUsersHandler;
            this._getAllCoursesHandler = getAllCoursesHandler;
            this._getCoursesPerEnrollHandler = getCoursesPerEnrollHandler;
            this._getUsersEnrolledInHandler = getUsersEnrolledInHandler;
            this._getUsersPerNumMessageHandler = getUsersPerNumMessageHandler;
        }


        public async Task ShowAsync(UserDTO currUser)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Statistics Menu ===");
                Console.WriteLine("1. Number of users per role");
                Console.WriteLine("2. Number of courses");
                Console.WriteLine("3. Top 3 courses by number of enrollments");
                Console.WriteLine("4. Top 3 users by number of messages");
                Console.WriteLine("0. Back to Admin Menu");
                Console.Write("Select an option: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        await ViewUsersPerRoleAsync();
                        break;
                    case '2':
                        await ViewNumOfCourses();
                        break;
                    case '3':
                        await ViewTop3CoursesByEnrollments();
                        break;
                    case '4':
                        await ViewTop3UsersByMessagesAsync();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        private async Task ViewUsersPerRoleAsync()
        {
            Console.Clear();
            var resUsers = await _getAllUsersHandler.HandleGetAllUsersAsync();
            if (resUsers.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("No users available");
            }
            var users = resUsers.Value.Items;

            var numStudents = users.Count(u => !u.isProfessor && !u.isAdministrator);
            var numProfessors = users.Count(u => u.isProfessor);
            var numAdministrators = users.Count(u => u.isAdministrator);

            Console.WriteLine("=== Users per Role ===");
            Console.WriteLine($"Students: {numStudents}");
            Console.WriteLine($"Professors: {numProfessors}");
            Console.WriteLine($"Administrators: {numAdministrators}");
            Console.WriteLine("Press any key to return to the Statistics Menu...");
            Console.ReadKey();
        }
        private async Task ViewNumOfCourses()
        {
            Console.Clear();
            var resCourses = await _getAllCoursesHandler.HandleGetAllCoursesAsync();
            var courses = resCourses.Value.Items;

            Console.WriteLine("=== Number of Courses ===");
            Console.WriteLine($"Total Courses: {courses.Count}");
            Console.WriteLine("Press any key to return to the Statistics Menu...");
            Console.ReadKey();
        }
        private async Task ViewTop3CoursesByEnrollments()
        {
            Console.Clear();
            var res = await _getCoursesPerEnrollHandler.HandleGetCoursesPerEnrollAsync();
            if (res.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("No course statistics available");
                return;
            }
            var courses = res.Value.Items;
            var coursesDict = new Dictionary<CourseDTO, int>();
            foreach (var c in courses)
            {
                var courseRes = await _getUsersEnrolledInHandler
                    .HandleGetUsersAsync(c.Id);
                if (courseRes.Value.isEmpty) continue;
                coursesDict[c] = courseRes.Value.Items.Count();
            }

            Console.WriteLine("=== Top 3 Courses by Enrollments ===");
            foreach (var course in coursesDict.OrderByDescending(c => c.Value).Take(3))
            {
                Console.WriteLine($"Course: {course.Key.Name}, Enrollments: {course.Value}");
            }
            Console.WriteLine("Press any key to return to the Statistics Menu...");
            Console.ReadKey();
        }
        private async Task ViewTop3UsersByMessagesAsync()
        {
            var choices = new Dictionary<int, DateTimeOffset>
            {
                { 1, DateTimeOffset.UtcNow.Date },
                { 2, new DateTimeOffset(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1, 0, 0, 0, TimeSpan.Zero) },
                { 3, DateTimeOffset.MinValue }
            };
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select Time Frame:");
                Console.WriteLine("1. Last Day");
                Console.WriteLine("2. Last Month");
                Console.WriteLine("3. All Time");
                var choice = Helper.Helper.getAndValidateInputInt(" an option or 0 to go back: ");
                if (choice == 0) return;
                if (!choices.ContainsKey(choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }
                var selectedDate = choices[choice];
                var res = await _getUsersPerNumMessageHandler.HandleGetUsersPerNumMessageAsync(selectedDate);
                if (res.Value.isEmpty)
                {
                    Helper.Helper.clearDisplAndDisplMessage("No user message statistics available");
                    return;
                }
                var users = res.Value.Items;
                Console.WriteLine("=== Top 3 Users by Number of Messages ===");
                foreach (var userStat in users.OrderByDescending(u => u.numMessages).Take(3))
                {
                    Console.WriteLine($"User ID: {userStat.userId}, Messages: {userStat.numMessages}, Email: {userStat.email}");
                }
                Console.ReadKey();
            }
        }
    }
}
