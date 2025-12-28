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
        public ManageCourseMenu(GetProfessorCoursesHandler getProfessorCoursesHandler 
            , GetAllStudentsHandler getAllStudentsHandler,
            AddStudentHandler addStudentHandler)
        {
            _getProfessorCoursesHandler = getProfessorCoursesHandler;
            _getAllStudentsHandler = getAllStudentsHandler;
            _addStudentHandler = addStudentHandler;
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
                        // Logic to view course materials and notifications
                        break;
                    case '3':
                        // Logic to add materials
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("All Students:");
                var number = 1;
                foreach (var student in res.Value.Items.OrderBy(s => s.Email))
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
                var selectedStudent = res.Value.Items[input - 1];
                var addRes = await _addStudentHandler.HandleAddStudentAsync(selectedStudent.Id, course.Id);
                if (addRes.Value.IsSuccess)
                {
                    Helper.Helper.clearDisplAndDisplMessage($"Student {selectedStudent.FirstName} added to course {course.Name} successfully.");
                }
                else
                {
                    Helper.Helper.displayValidationErrors(addRes.Errors);
                }
            }
        }
        private async Task AddNotifs(UserDTO currUser, CourseDTO course) { 
            
        }
    }
}
