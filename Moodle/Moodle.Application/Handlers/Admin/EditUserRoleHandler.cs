using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Admin
{
    public class EditUserRoleHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMoodleDbContext _context;
        public EditUserRoleHandler(IUserRepository userRepository , IMoodleDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        public async Task<Result<SuccessResponse>> HandleEditUserRoleAsync(int userId)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteEditUserRoleAsync(userId, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteEditUserRoleAsync(int userId,  Result<SuccessResponse> res)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                res.setValue(new SuccessResponse { IsSuccess = false });
                return res;
            }
            if (!user.IsProfessor)
            {
                user.IsProfessor = !user.IsProfessor;
                _userRepository.Update(user);
                await _context.SaveChangesAsync();
                res.setValue(new SuccessResponse { IsSuccess = true });
                return res;
            }
            var courses = await _userRepository.GetManagedByAsync(userId);
            if (courses == null || courses.Count == 0)
            {
                user.IsProfessor = !user.IsProfessor;
                _userRepository.Update(user);
                await _context.SaveChangesAsync();
                res.setValue(new SuccessResponse { IsSuccess = true });
                return res;
            }
            var validationResult = new Domain.Common.Validation.ValidationResult();
            validationResult.AddValidationItem(ValidationItems.User.UserEdit);
            res.setValidationResult(validationResult);
            res.setValue(new SuccessResponse { IsSuccess = false });
            return res;
        }
    }
}
