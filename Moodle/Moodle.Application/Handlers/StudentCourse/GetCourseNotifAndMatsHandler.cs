using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.StudentCourse
{
    public class GetCourseNotifAndMatsHandler
    {
        private readonly ICourseRepository _courseRepository;
        public GetCourseNotifAndMatsHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Result<GetAllResponse<CourseNotifDTO>>> HandleGetNotifsAsync(int courseId)
        {
            var res = new Result<GetAllResponse<CourseNotifDTO>>();
            return await ExecuteGetNotifsAsync(courseId, res);
        }
        private async Task<Result<GetAllResponse<CourseNotifDTO>>> ExecuteGetNotifsAsync(int courseId, Result<GetAllResponse<CourseNotifDTO>> res)
        {
            var notifs = await _courseRepository.GetNotifsByCourseAsync(courseId);
            var professor = await _courseRepository.GetProfessor(courseId);
            if (notifs == null || notifs.Count == 0 || professor == null)
            {
                res.setValue(new GetAllResponse<CourseNotifDTO> {Items = new List<CourseNotifDTO>() });
                return res;
            }
            var notifDtos = notifs.Select(n => new CourseNotifDTO(n) { ProfessorEmail = professor.Email }).ToList();
            res.setValue(new GetAllResponse<CourseNotifDTO> { Items = notifDtos });
            return res;
        }

        public async Task<Result<GetAllResponse<MaterialsDTO>>> HandleGetMatsAsync(int courseId)
        {
            var res = new Result<GetAllResponse<MaterialsDTO>>();
            return await ExecuteGetMatsAsync(courseId, res);
        }
        private async Task<Result<GetAllResponse<MaterialsDTO>>> ExecuteGetMatsAsync(int courseId, Result<GetAllResponse<MaterialsDTO>> res)
        {
            var mats = await _courseRepository.GetMaterialsByCourseAsync(courseId);
            if (mats == null || mats.Count == 0)
            {
                res.setValue(new GetAllResponse<MaterialsDTO> { Items = new List<MaterialsDTO>() });
                return res;
            }
            var matDtos = mats.Select(m => new MaterialsDTO(m)).ToList();
            res.setValue(new GetAllResponse<MaterialsDTO> { Items = matDtos });
            return res;
        }
    }
}
