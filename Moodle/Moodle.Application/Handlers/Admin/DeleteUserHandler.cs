using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Admin
{
    public class DeleteUserHandler
    {
        private readonly IMoodleDbContext _context;
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IMoodleDbContext context , IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Result<SuccessResponse>> HandleDeleteUserAsync(int userId)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteDeleteUserAsync(userId, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteDeleteUserAsync(int userId, Result<SuccessResponse> res)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                res.setValue(new SuccessResponse { IsSuccess = false });
                return res;
            }
            if (!user.IsProfessor)
            {
                _userRepository.Delete(user);
                await _context.SaveChangesAsync();
                res.setValue(new SuccessResponse { IsSuccess = true });
                return res;
            }
            var courses = await _userRepository.GetManagedByAsync(userId);
            if (courses == null || courses.Count == 0)
            {
                _userRepository.Delete(user);
                await _context.SaveChangesAsync();
                res.setValue(new SuccessResponse { IsSuccess = true });
                return res;
            }
            var valdationResult = new Domain.Common.Validation.ValidationResult();
            valdationResult.AddValidationItem(ValidationItems.User.UserDelete);
            res.setValidationResult(valdationResult);
            res.setValue(new SuccessResponse { IsSuccess = false });
            return res;
        }

    }
}
