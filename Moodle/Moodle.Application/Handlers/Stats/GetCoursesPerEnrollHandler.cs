using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Stats
{
    public class GetCoursesPerEnrollHandler
    {
        private readonly ICourseRepository _courseRepository;
        public GetCoursesPerEnrollHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Result<GetAllResponse<CourseDTO>>> HandleGetCoursesPerEnrollAsync()
        {
            var res = new Result<GetAllResponse<CourseDTO>>();
            return await ExecuteGetCoursesPerEnrollAsync(res);
        }
        private async Task<Result<GetAllResponse<CourseDTO>>> ExecuteGetCoursesPerEnrollAsync(Result<GetAllResponse<CourseDTO>> res)
        {
            var courses = await _courseRepository.GetTop3CoursesByEnrollmentsAsync();
            if (courses == null || !courses.Any())
            {
                res.setValue(new GetAllResponse<CourseDTO> { Items = new List<CourseDTO>() });
                return res;
            }
            var courseDTOs = courses.Select(c => new CourseDTO(c)).ToList();
           
            res.setValue(new GetAllResponse<CourseDTO> { Items = courseDTOs });
            return res;
        }
    }
}
