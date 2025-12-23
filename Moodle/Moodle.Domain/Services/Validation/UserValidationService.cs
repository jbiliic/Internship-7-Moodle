using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Domain.Services.Validation
{
    public class UserValidationService
    {
        private readonly IUserRepository _userRepository;

        public UserValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult>ValidateUser(User user) { 
            var res = user.ValidateBasic();
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser is not null)
            {
                res.AddValidationItem(
                    ValidationItems.User.EmailExists
                );
            }
            if (!IsValidPassword(user.Password))
            {
                res.AddValidationItem(
                    ValidationItems.User.PasswError
                );
            }
            return res;
        }
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            if (password.Length < 6)
                return false;
            var hasNumber = password.Any(char.IsDigit);
            var hasUpperChar = password.Any(char.IsUpper);
            return hasNumber && hasUpperChar;
        }
    }
}
