using Moodle.Application.DTO;
using Moodle.Application.Handlers.Professor;
using Moodle.Application.Handlers.StudentCourse;

namespace Moodle.Presentation.Menus.Professor
{
    internal class ManageCourseMenu : IMenu
    {
        private readonly GetProfessorCoursesHandler _getProfessorCoursesHandler;
        private readonly GetAllStudentsHandler _getAllStudentsHandler;
        private readonly AddStudentHandler _addStudentHandler;
        private readonly AddNotifAndMatsHandler _addNotifAndMatsHandler;
        public ManageCourseMenu(GetProfessorCoursesHandler getProfessorCoursesHandler 
            , GetAllStudentsHandler getAllStudentsHandler,
            AddStudentHandler addStudentHandler,
            AddNotifAndMatsHandler addNotifAndMatsHandler)
        {
            _getProfessorCoursesHandler = getProfessorCoursesHandler;
            _getAllStudentsHandler = getAllStudentsHandler;
            _addStudentHandler = addStudentHandler;
            _addNotifAndMatsHandler = addNotifAndMatsHandler;
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
                await ManageCourseAsync(currUser, selectedCourse);
            }
        }
        private async Task ManageCourseAsync(UserDTO currUser, CourseDTO course)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Managing Course: {course.Name}");
                Console.WriteLine("1. Add Students");
                Console.WriteLine("2. Add Notifications");
                Console.WriteLine("3. Add Materials");
                Console.WriteLine("0. Back to Previous Menu");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        await AddStudentsAsync(currUser, course);
                        break;
                    case '2':
                        await AddNotifs(currUser, course);
                        break;
                    case '3':
                        await AddMats(currUser, course);
                        break;
                    case '0':
                        return;
                    default:
                        Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }
        private async Task AddStudentsAsync(UserDTO currUser, CourseDTO course)
        {
            var res = await _getAllStudentsHandler.HandleGetAllStudents();
            if (res.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("Wow,such empty");
                return;
            }
            var students = new List<UserDTO>(res.Value.Items.OrderBy(s => s.Email));
            while (true)
            {
                Console.Clear();
                Console.WriteLine("All Students:");
                var number = 1;
                foreach (var student in students)
                {
                    Console.WriteLine($"{number++}. {student.FirstName} ({student.Email})");
                }
                var input = Helper.Helper.getAndValidateInputInt(" a student to add or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > res.Value.Items.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }
                var selectedStudent = students.ElementAt(input - 1);
                var addRes = await _addStudentHandler.HandleAddStudentAsync(selectedStudent.Id, course.Id);
                if (addRes.Value.IsSuccess)
                {
                    Helper.Helper.clearDisplAndDisplMessage($"Student {selectedStudent.FirstName} added to course {course.Name} successfully.");
                    students.RemoveAt(input - 1);
                }
                else
                {
                    Helper.Helper.displayValidationErrors(addRes.Errors);
                }
            }
        }
        private async Task AddNotifs(UserDTO currUser, CourseDTO course) 
        { 
            Console.Clear();
            var title = Helper.Helper.getStringOptional("notification title");
            var content = Helper.Helper.getStringOptional("notification content");
            var res = await _addNotifAndMatsHandler.HandleAddNotificationAsync(new CourseNotifDTO { Title = title, Content = content }, course.Id, currUser.Id);
            if (res.Value.IsSuccess)
            {
                Helper.Helper.clearDisplAndDisplMessage($"Notification '{title}' added successfully.");
            }
            else
            {
                Helper.Helper.displayValidationErrors(res.Errors);
            }
        }
        private async Task AddMats(UserDTO currUser, CourseDTO course)
        {
            Console.Clear();
            var title = Helper.Helper.getStringOptional("material title");
            var url = Helper.Helper.getStringOptional("material url");
            var res = await _addNotifAndMatsHandler.HandleAddMatsAsync(new MaterialsDTO { Title = title, FilePath = url }, course.Id, currUser.Id);
            if (res.Value.IsSuccess)
            {
                Helper.Helper.clearDisplAndDisplMessage($"Material '{title}' added successfully.");
            }
            else
            {
                Helper.Helper.displayValidationErrors(res.Errors);
            }
        }
    }
}
