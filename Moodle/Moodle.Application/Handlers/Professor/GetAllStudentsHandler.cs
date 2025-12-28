using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Professor
{
    public class GetAllStudentsHandler
    {
        private readonly IUserRepository _userRepository;
        public GetAllStudentsHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<GetAllResponse<UserDTO>>> HandleGetAllStudents() { 
            var res = new Result<GetAllResponse<UserDTO>>();
            return await ExecuteGetAllStudents(res);
        }
        private async Task<Result<GetAllResponse<UserDTO>>> ExecuteGetAllStudents(Result<GetAllResponse<UserDTO>> res)
        {
            var students = await _userRepository.GetAllStudentsAsync();
            if (students == null || students.Count == 0)
            {
                res.setValue(new GetAllResponse<UserDTO> { Items = new List<UserDTO>() });
                return res;
            }
            var studentDtos = students.Select(s => new UserDTO(s)).ToList();
            res.setValue(new GetAllResponse<UserDTO> { Items = studentDtos });
            return res;
        }

    }
}
