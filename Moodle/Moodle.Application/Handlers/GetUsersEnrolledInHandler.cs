using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers
{
    public class GetUsersEnrolledInHandler
    {
        private readonly IUserRepository _userRepository;
        public GetUsersEnrolledInHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<GetAllResponse<UserDTO>>> HandleGetUsersAsync(int courseId)
        {
            var res = new Result<GetAllResponse<UserDTO>>();
            return await ExecuteGetUsersAsync( courseId , res);
        }
        private async Task<Result<GetAllResponse<UserDTO>>> ExecuteGetUsersAsync(int courseId , Result<GetAllResponse<UserDTO>> res)
        {
            var users = await _userRepository.GetUsersEnrolledInByCourseIdAsync(courseId);
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
