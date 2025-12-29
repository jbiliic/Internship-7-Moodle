using System.Text.RegularExpressions;
using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Admin
{
    public class EditUserEmailHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMoodleDbContext _context;
        private readonly string RegexMailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public EditUserEmailHandler(IUserRepository userRepository, IMoodleDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        public async Task<Result<SuccessResponse>> HandleEditUserEmailAsync(int userId, string newEmail)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteEditUserEmailAsync(userId, newEmail, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteEditUserEmailAsync(int userId, string newEmail, Result<SuccessResponse> res)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                res.setValue(new SuccessResponse { IsSuccess = false });
                return res;
            }

            if (!Regex.IsMatch(newEmail, RegexMailPattern))
            { 
                var validationResult = new Domain.Common.Validation.ValidationResult();
                validationResult.AddValidationItem(ValidationItems.User.EmailInvalidFormat);
                res.setValidationResult(validationResult);
                res.setValue(new SuccessResponse { IsSuccess = false });
                return res;
            }

            var emailExists = await _userRepository.GetByEmailAsync(newEmail);
            if (emailExists != null )
            {
                var validationResult = new Domain.Common.Validation.ValidationResult();
                validationResult.AddValidationItem(ValidationItems.User.EmailExists);
                res.setValidationResult(validationResult);
                res.setValue(new SuccessResponse { IsSuccess = false });
                return res;
            }

            user.Email = newEmail;
            _userRepository.Update(user);
            await _context.SaveChangesAsync();
            res.setValue(new SuccessResponse { IsSuccess = true });
            return res;
        }
    }
}
