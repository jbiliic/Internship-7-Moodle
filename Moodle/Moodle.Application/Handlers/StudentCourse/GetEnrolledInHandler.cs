using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.StudentCourse
{
    public class GetEnrolledInHandler
    {
        private readonly IUserRepository _userRepository;
        public GetEnrolledInHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<GetAllResponse<CourseDTO>>> HandleGetEnrolledIn(int userId)
        {
            var res = new Result<GetAllResponse<CourseDTO>>();
            return await ExecuteGetEnrolledCoursesAsync(userId,res);
        }
        private async Task<Result<GetAllResponse<CourseDTO>>> ExecuteGetEnrolledCoursesAsync(int userId , Result<GetAllResponse<CourseDTO>> res )
        {
            var courses = await _userRepository.GetEnrolledInAsync(userId);
            if (courses == null || !courses.Any())
            {
                res.setValue(new GetAllResponse<CourseDTO> {Items = Array.Empty<CourseDTO>() });
                return res;
            }
            var courseDtos = courses.Select(c => new CourseDTO(c)).ToList();
            res.setValue(new GetAllResponse<CourseDTO> { Items = courseDtos });
            return res;
        }
    }
}
