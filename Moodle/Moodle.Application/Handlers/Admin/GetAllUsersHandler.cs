using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Admin
{
    public class GetAllUsersHandler
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<GetAllResponse<UserDTO>>> HandleGetAllUsersAsync()
        {
            var res = new Result<GetAllResponse<UserDTO>>();
            return await ExecuteGetAllUsersAsync(res);
        }
        private async Task<Result<GetAllResponse<UserDTO>>> ExecuteGetAllUsersAsync(Result<GetAllResponse<UserDTO>> res)
        {
            var users = await _userRepository.GetAllAsync();
            if (users == null || users.Count == 0)
            {
                res.setValue(new GetAllResponse<UserDTO>() { Items = new List<UserDTO>() });
                return res;
            }
            var userDtos = users.Select(u => new UserDTO(u)).ToList();
            res.setValue(new GetAllResponse<UserDTO>() { Items = userDtos });
            return res;
        }
    }
}
