using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Application.DTO.Auth;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Auth
{
    public class LoginReqHandler
    {
        private readonly IUserRepository _userRepo;
        public LoginReqHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        private async Task<Resault<SuccessResponse<UserDTO>>> ExecuteLogin(UserLoginReq req , Resault<SuccessResponse<UserDTO>> res)
        {
            var passw = req.Password;
            var email = req.Email;
            var user = await _userRepo.AuthenticateUserAsync(email, passw);
            if (user == null)
            {
                res.setValue(new SuccessResponse<UserDTO> {IsSuccess = false , Item = null });
                return res;
            }
            var userDto = new UserDTO(user);
            res.setValue(new SuccessResponse<UserDTO> { IsSuccess = true, Item = userDto , Id = user.Id });
            return res;
        }
        public async Task<Resault<SuccessResponse<UserDTO>>> HandleLogin(UserLoginReq req)
        {
            var res = new Resault<SuccessResponse<UserDTO>>();
            return await ExecuteLogin(req, res);
        }
    }
}
