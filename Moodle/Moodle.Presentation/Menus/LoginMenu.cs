using Moodle.Application.DTO;
using Moodle.Application.Handlers.Auth;
namespace Moodle.Presentation.Menus
{
    internal class LoginMenu : IMenu
    {
        private readonly LoginReqHandler _loginHandler;
        private readonly RegistrationReqHandler _registrationHandler;
        private readonly MenuRouter _router;
        public LoginMenu(LoginReqHandler loginHandler, RegistrationReqHandler registrationHandler , MenuRouter router)
        {
            _loginHandler = loginHandler;
            _registrationHandler = registrationHandler;
            _router = router;
        }
        public async Task ShowAsync(UserDTO? currUser)
        {
            currUser = null;
            while (true)
            {
                Console.WriteLine("Welcome to Moodle!");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        currUser =  await LoginSubMenuAsync();
                        if (currUser == null)
                            break;
                        await _router.NavigateTo<MainMenu>(currUser);
                        break;
                    case '2':
                        currUser = await RegisterSubMenuAsync();
                        if (currUser == null)
                            break;
                        await _router.NavigateTo<MainMenu>(currUser);
                        break;
                    case '0':
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }
        public async Task<UserDTO?> LoginSubMenuAsync() {
            var email = Helper.Helper.getString("email");
            var passw = Helper.Helper.getString("password");
            var res = await _loginHandler.HandleLogin(new Application.DTO.Auth.UserLoginReq { Email = email, Password = passw });
            if (!res.Value.IsSuccess) {
                Helper.Helper.clearDisplAndDisplMessage("Login failed. Please check your credentials and try again.");
                Helper.Helper.timeOut30s();
                return null;
            }
            Helper.Helper.clearDisplAndDisplMessage($"Login successful! Welcome, {res.Value.Item.FirstName}.");
            return res.Value.Item;
        }
        public async Task<UserDTO?> RegisterSubMenuAsync() {
            var firstName = Helper.Helper.getStringOptional("first name");
            var lastName = Helper.Helper.getStringOptional("last name");
            var email = Helper.Helper.getString("email");
            var password = Helper.Helper.getString("password");
            var confirmPassword = Helper.Helper.getString("confirm password");
            var birthDate = Helper.Helper.getDateOfBirth("birth date (yyyy-MM-dd)");
            var randStr = Helper.Helper.generateCaptcha(6);
            var input = Helper.Helper.getString($"captcha: {randStr}");
            if (!input.Equals(randStr))
            {
                Helper.Helper.clearDisplAndDisplMessage("Captcha incorrect.");
                return null;
            }
            if (!password.Equals(confirmPassword))
            {
                Helper.Helper.clearDisplAndDisplMessage("Passwords do not match.");
                return null;
            }
            var res = await _registrationHandler.HandleRegistration(new Application.DTO.Auth.UserRegistrationReq
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                BirthDate = birthDate
            });
            if (!res.Value.IsSuccess)
            {
                Helper.Helper.clearDisplAndDisplMessage("Registration failed. Please check your details and try again.");
                Helper.Helper.displayValidationErrors(res.Errors);
                return null;
            }
                Helper.Helper.clearDisplAndDisplMessage("Registration successful! You can now log in with your credentials.");
            return res.Value.Item;
        }
    }
}
