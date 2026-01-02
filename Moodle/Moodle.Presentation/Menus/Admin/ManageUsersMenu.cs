using Moodle.Application.DTO;
using Moodle.Application.Handlers.Admin;
using Moodle.Application.Handlers.Professor;

namespace Moodle.Presentation.Menus.Admin
{
    internal class ManageUsersMenu : IMenu
    {
        private readonly DeleteUserHandler _deleteUserHandler;
        private readonly GetAllUsersHandler _getAllUsersHandler;
        private readonly EditUserRoleHandler _editUserRoleHandler;
        private readonly EditUserEmailHandler _editUserEmailHandler;
        private readonly GetAllProfHandler _getAllProfHandler;
        private readonly GetAllCoursesHandler _getAllCoursesHandler;
        private readonly ReassignProfHandler _reassignProfHandler;
        public ManageUsersMenu(DeleteUserHandler deleteUserHandler,
            GetAllUsersHandler getAllUsersHandler,
            EditUserRoleHandler editUserRoleHandler,
            EditUserEmailHandler editUserEmailHandler,
            GetAllProfHandler getAllProfHandler,
            GetAllCoursesHandler getAllCoursesHandler,
            ReassignProfHandler reassignProfHandler)
        {
            _deleteUserHandler = deleteUserHandler;
            _getAllUsersHandler = getAllUsersHandler;
            _editUserRoleHandler = editUserRoleHandler;
            _editUserEmailHandler = editUserEmailHandler;
            _getAllProfHandler = getAllProfHandler;
            _getAllCoursesHandler = getAllCoursesHandler;
            _reassignProfHandler = reassignProfHandler;
        }
        public async Task ShowAsync(UserDTO user)
        {
            while (true) { 
                Console.Clear();
                Console.WriteLine("=== Manage Users Menu ===");
                Console.WriteLine("1. Delete User");
                Console.WriteLine("2. Edit User Role");
                Console.WriteLine("3. Edit User Email");
                Console.WriteLine("4. Reassign Professor to Course");
                Console.WriteLine("0. Back");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        await DeleteUserAsync();
                        break;
                    case '2':
                        await EditUserRoleAsync();
                        break;
                    case '3':
                        await EditUserEmailAsync();
                        break;
                    case '4':
                        await ReassignProfessorAsync();
                        break;
                    case '0':
                        return;
                    default:
                        Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }
        private async Task DeleteUserAsync()
        {
            Console.Clear();
            var res = await _getAllUsersHandler.HandleGetAllUsersAsync();
            if (res.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("No users found to delete.");
                return;
            }
            var users = new List<UserDTO>(res.Value.Items.OrderBy(s => s.Email));
            while (true)
            {
                Console.Clear();
                Console.WriteLine("All Users:");
                var number = Helper.Helper.displayUsers(users);
                var input = Helper.Helper.getAndValidateInputInt(" a user to delete or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > users.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }

                if (!Helper.Helper.waitForConfirmation()) continue;

                var selectedUser = users.OrderBy(s => s.Email).ElementAt(input - 1);
                var addRes = await _deleteUserHandler.HandleDeleteUserAsync(selectedUser.Id);
                if (addRes.Value.IsSuccess)
                {
                    Helper.Helper.clearDisplAndDisplMessage("User deleted successfully.");
                    users.Remove(selectedUser);
                }
                else if (addRes.hasErrors)
                {
                    Helper.Helper.displayValidationErrors(addRes.Errors);
                }
                else
                {
                    Helper.Helper.clearDisplAndDisplMessage("User doesnt exist.");

                }
            }
        }
        private async Task EditUserRoleAsync()
        {
            Console.Clear();
            var res = await _getAllUsersHandler.HandleGetAllUsersAsync();
            if (res.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("No users found to edit.");
                return;
            }
            var students = new List<UserDTO>(res.Value.Items.Where(u => u.isProfessor == false && u.isAdministrator == false).OrderBy(s => s.Email));
            var professors = new List<UserDTO>(res.Value.Items.Where(u => u.isProfessor).OrderBy(s => s.Email));
            while (true)
            {
                Console.Clear();
                Console.WriteLine("All Students:");
                var number = 1;
                foreach (var student in students)
                {
                    Console.WriteLine($"{number++}. {student.FirstName} ({student.Email})");
                }
                Console.WriteLine("All Professors:");
                foreach (var professor in professors)
                {
                    Console.WriteLine($"{number++}. {professor.FirstName} ({professor.Email})");
                }
                var input = Helper.Helper.getAndValidateInputInt(" a user to edit or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > res.Value.Items.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }

                if (!Helper.Helper.waitForConfirmation()) continue;

                var users = new List<UserDTO>(students);
                users.AddRange(professors);

                var selectedUser = users.ElementAt(input - 1);
                var addRes = await _editUserRoleHandler.HandleEditUserRoleAsync(selectedUser.Id);
                if (addRes.Value.IsSuccess)
                {
                    Helper.Helper.clearDisplAndDisplMessage("User role edited successfully.");
                    if (selectedUser.isProfessor)
                    {
                        selectedUser.isProfessor = false;
                        students.Add(selectedUser);
                        professors.Remove(selectedUser);
                    }
                    else
                    {
                        selectedUser.isProfessor = true;
                        professors.Add(selectedUser);
                        students.Remove(selectedUser);
                    }
                }
                else if (addRes.hasErrors)
                {
                    Helper.Helper.displayValidationErrors(addRes.Errors);
                }
                else
                {
                    Helper.Helper.clearDisplAndDisplMessage("User doesnt exist.");

                }
            }
        }
        private async Task EditUserEmailAsync()
        {
            Console.Clear();
            var res = await _getAllUsersHandler.HandleGetAllUsersAsync();
            if (res.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("No users found to edit.");
                return;
            }

            var users = new List<UserDTO>(res.Value.Items.OrderBy(s => s.Email));
            while (true)
            {
                Console.Clear();
                Console.WriteLine("All Users:");
                var number = Helper.Helper.displayUsers(users);
                var input = Helper.Helper.getAndValidateInputInt(" a user to edit or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > res.Value.Items.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }

                var selectedUser = users.ElementAt(input - 1);
                var newEmail = Helper.Helper.getString("the new email: ");
                if(!Helper.Helper.waitForConfirmation()) continue;

                var editEmailRes = await _editUserEmailHandler.HandleEditUserEmailAsync(selectedUser.Id, newEmail);
                if (editEmailRes.Value.IsSuccess)
                {
                    Helper.Helper.clearDisplAndDisplMessage("User email edited successfully.");
                    selectedUser.Email = newEmail;
                }
                else if (editEmailRes.hasErrors)
                {
                    Helper.Helper.displayValidationErrors(editEmailRes.Errors);
                }
                else
                {
                    Helper.Helper.clearDisplAndDisplMessage("User doesnt exist.");
                }
            }
        }
        private async Task ReassignProfessorAsync()
        {
            Console.Clear();

            var resCourses = await _getAllCoursesHandler.HandleGetAllCoursesAsync();
            if (resCourses.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("No courses found to edit.");
                return;
            }

            var resProfessors = await _getAllProfHandler.HandleGetAllProfAsync();
            if (resProfessors.Value.isEmpty)
            {
                Helper.Helper.clearDisplAndDisplMessage("No professors found to assign.");
                return;
            }
            
            var courses = new List<CourseDTO>(resCourses.Value.Items.OrderBy(c => c.Name));
            var professors = new List<UserDTO>(resProfessors.Value.Items.OrderBy(c => c.Email));

            while (true)
            {
                Console.WriteLine("All Courses:");
                var number = 1;
                foreach (var course in courses)
                {
                    Console.WriteLine($"{number++}. {course.Name}");
                }
                var input = Helper.Helper.getAndValidateInputInt(" a course to reassign or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > courses.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }
                var selectedCourse = courses.OrderBy(c => c.Name).ElementAt(input - 1);


                Console.Clear();
                Console.WriteLine("All Professors:");
                number = Helper.Helper.displayUsers(professors);
                input = Helper.Helper.getAndValidateInputInt(" a professor to assign or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > professors.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }
                var selectedProfessor = professors.ElementAt(input - 1);
                
                if (!Helper.Helper.waitForConfirmation()) continue;

                var reassignRes = await _reassignProfHandler.HandleReassignProfAsync(selectedCourse.Id, selectedProfessor.Id);
                if (reassignRes.Value.IsSuccess)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Professor reassigned successfully.");
                }
                else if (reassignRes.hasErrors)
                {
                    Helper.Helper.displayValidationErrors(reassignRes.Errors);
                }
                else
                {
                    Helper.Helper.clearDisplAndDisplMessage("Course doesnt exist.");

                }  
            }
        }
    }
}
