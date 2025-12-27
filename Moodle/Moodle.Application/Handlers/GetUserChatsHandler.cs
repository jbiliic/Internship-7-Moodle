using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Application.DTO.Auth;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers
{
    public class GetUserChatsHandler
    {
        private readonly IUserRepository _userRepo;
        public GetUserChatsHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<Resault<GetAllResponse<UserDTO>>> HandleGetChats(int userId)
        {
            var res = new Resault<GetAllResponse<UserDTO>>();
            return await ExecuteGetChats(userId, res);
        }
        private async Task<Resault<GetAllResponse<UserDTO>>> ExecuteGetChats(int userId, Resault<GetAllResponse<UserDTO>> res)
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

        public async Task<Resault<GetAllResponse<UserDTO>>> HandleGetWOChats(int userId)
        {
            var res = new Resault<GetAllResponse<UserDTO>>();
            return await ExecuteGetWOChats(userId, res);
        }
        private async Task<Resault<GetAllResponse<UserDTO>>> ExecuteGetWOChats(int userId, Resault<GetAllResponse<UserDTO>> res)
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
