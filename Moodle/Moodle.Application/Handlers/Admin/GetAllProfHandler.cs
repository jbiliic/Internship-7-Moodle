using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Admin
{
    public class GetAllProfHandler
    {
        private readonly IUserRepository _userRepository;
        public GetAllProfHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<GetAllResponse<UserDTO>>> HandleGetAllProfAsync()
        {
            var res = new Result<GetAllResponse<UserDTO>>();
            return await ExecuteGetAllProfAsync(res);
        }
        private async Task<Result<GetAllResponse<UserDTO>>> ExecuteGetAllProfAsync(Result<GetAllResponse<UserDTO>> res)
        {
            var profs = await _userRepository.GetAllProfessorsAsync();
            if (profs == null || profs.Count == 0)
            {
                res.setValue(new GetAllResponse<UserDTO>(new List<UserDTO>()));
                return res;
            }
            var profDtos = profs.Select(p => new UserDTO(p)).ToList();
            res.setValue(new GetAllResponse<UserDTO>(profDtos));
            return res;
        }
    }
}
