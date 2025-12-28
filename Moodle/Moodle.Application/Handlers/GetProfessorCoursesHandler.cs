using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers
{
    public class GetProfessorCoursesHandler
    {
        private readonly ICourseRepository _courseRepository;
        public GetProfessorCoursesHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Result<GetAllResponse<CourseDTO>>> HandleGetManagedByProf(int professorId)
        {
            var res = new Result<GetAllResponse<CourseDTO>>();
            return await ExecuteGetManagedByProf(professorId, res);
        }
        private async Task<Result<GetAllResponse<CourseDTO>>> ExecuteGetManagedByProf(int professorId, Result<GetAllResponse<CourseDTO>> res)
        {
            var courses = await _courseRepository.GetCoursesManagedByProfessor(professorId);
            if (courses == null || courses.Count == 0)
            {
                res.setValue(new GetAllResponse<CourseDTO> { Items = new List<CourseDTO>() });
                return res;
            }
            var courseDtos = courses.Select(c => new CourseDTO(c)).ToList();
            res.setValue(new GetAllResponse<CourseDTO> {Items = courseDtos });
            return res;
        }
    }
}
