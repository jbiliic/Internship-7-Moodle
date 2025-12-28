using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Application.DTO.Auth;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Convo
{
    public class GetUserChatsHandler
    {
        private readonly IUserRepository _userRepo;
        public GetUserChatsHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<Result<GetAllResponse<UserDTO>>> HandleGetChats(int userId)
        {
            var res = new Result<GetAllResponse<UserDTO>>();
            return await ExecuteGetChats(userId, res);
        }
        private async Task<Result<GetAllResponse<UserDTO>>> ExecuteGetChats(int userId, Result<GetAllResponse<UserDTO>> res)
        {
            var users = await _userRepo.GetUsersWithConversation(userId);
            if (users == null || users.Count == 0)
            {
                res.setValue(new GetAllResponse<UserDTO> { Items = new List<UserDTO>() });
                return res;
            }
            var userDtos = users.Select(u => new UserDTO(u)).ToList();
            res.setValue(new GetAllResponse<UserDTO> { Items = userDtos });
            return res;
        }

        public async Task<Result<GetAllResponse<UserDTO>>> HandleGetWOChats(int userId)
        {
            var res = new Result<GetAllResponse<UserDTO>>();
            return await ExecuteGetWOChats(userId, res);
        }
        private async Task<Result<GetAllResponse<UserDTO>>> ExecuteGetWOChats(int userId, Result<GetAllResponse<UserDTO>> res)
        {
            var users = await _userRepo.GetUsersWOConversation(userId);
            if (users == null || users.Count == 0)
            {
                res.setValue(new GetAllResponse<UserDTO> { Items = new List<UserDTO>() });
                return res;
            }
            var userDtos = users.Select(u => new UserDTO(u)).ToList();
            res.setValue(new GetAllResponse<UserDTO> { Items = userDtos });
            return res;
        }

    }
}
