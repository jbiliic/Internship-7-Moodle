using Moodle.Application.DTO;
using Moodle.Application.Handlers.Admin;
using Moodle.Application.Handlers.Professor;

namespace Moodle.Presentation.Menus.Admin
{
    internal class ManageUsersMenu : IMenu
    {
        private readonly DeleteUserHandler _deleteUserHandler;
        private readonly GetAllUsersHandler _getAllUsersHandler;
        public ManageUsersMenu(DeleteUserHandler deleteUserHandler, GetAllUsersHandler getAllUsersHandler)
        {
            _deleteUserHandler = deleteUserHandler;
            _getAllUsersHandler = getAllUsersHandler;
        }
        public async Task ShowAsync(UserDTO user)
        {
            while (true) { 
                Console.Clear();
                Console.WriteLine("=== Manage Users Menu ===");
                Console.WriteLine("1. Delete User");
                Console.WriteLine("2. Edit User Role");
                Console.WriteLine("3. Edit User Email");
                Console.WriteLine("0. Back");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        // Call delete user method
                        break;
                    case '2':
                        // Call edit user role method
                        break;
                    case '3':
                        // Call edit user email method
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
            var users = new List<UserDTO>(res.Value.Items);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("All Users:");
                var number = 1;
                foreach (var user in res.Value.Items.OrderBy(s => s.Email))
                {
                    Console.WriteLine($"{number++}. {user.FirstName} ({user.Email})");
                }
                var input = Helper.Helper.getAndValidateInputInt(" a user to delete or 0 to go back: ");
                if (input == 0) return;
                if (input < 1 || input > res.Value.Items.Count)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                    continue;
                }
                var selectedUser = users.ElementAt(input - 1);
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
    }
}
