using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Admin
{
    public class GetAllCoursesHandler
    {
        private readonly ICourseRepository _courseRepository;
        public GetAllCoursesHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Result<GetAllResponse<CourseDTO>>> HandleGetAllCoursesAsync()
        {
            var res = new Result<GetAllResponse<CourseDTO>>();
            return await ExecuteGetAllCoursesAsync(res);
        }
        private async Task<Result<GetAllResponse<CourseDTO>>> ExecuteGetAllCoursesAsync(Result<GetAllResponse<CourseDTO>> res)
        {
            var courses = await _courseRepository.GetAllAsync();
            if (courses == null || courses.Count == 0)
            {
                res.setValue(new GetAllResponse<CourseDTO>() { Items = new List<CourseDTO>() });
                return res;
            }
            var courseDtos = courses.Select(c => new CourseDTO(c)).ToList();
            res.setValue(new GetAllResponse<CourseDTO>() { Items = courseDtos });
            return res;
        }
    }
}
