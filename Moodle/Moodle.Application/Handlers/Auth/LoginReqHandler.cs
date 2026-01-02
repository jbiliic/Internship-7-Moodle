using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Application.DTO.Auth;
using Moodle.Domain.Persistence.Repository;
using Moodle.Domain.Services.Cache;

namespace Moodle.Application.Handlers.Auth
{
    public class LoginReqHandler
    {
        private readonly IUserRepository _userRepo;
        public LoginReqHandler(IUserRepository userRepo )
        {
            _userRepo = userRepo;
        }
        private async Task<Result<SuccessResponseGet<UserDTO>>> ExecuteLogin(UserLoginReq req , Result<SuccessResponseGet<UserDTO>> res)
        {
            var passw = req.Password;
            var email = req.Email;
            var user = await _userRepo.AuthenticateUserAsync(email, passw);
            if (user == null)
            {
                res.setValue(new SuccessResponseGet<UserDTO> {IsSuccess = false , Item = null });
                return res;
            }
            var userDto = new UserDTO(user);
            res.setValue(new SuccessResponseGet<UserDTO> { IsSuccess = true, Item = userDto , Id = user.Id });
            return res;
        }
        public async Task<Result<SuccessResponseGet<UserDTO>>> HandleLogin(UserLoginReq req)
        {
            var res = new Result<SuccessResponseGet<UserDTO>>();
            return await ExecuteLogin(req, res);
        }
    }
}
