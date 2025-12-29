using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Admin
{
    public class ReassignProfHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMoodleDbContext _context;
        public ReassignProfHandler(IUserRepository userRepository 
            , IMoodleDbContext context
            , ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _context = context;
            _courseRepository = courseRepository;
        }
        public async Task<Result<SuccessResponse>> HandleReassignProfAsync(int profId, int courseId)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteReassignProfAsync(profId, courseId, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteReassignProfAsync(int profId, int courseId, Result<SuccessResponse> res)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                res.setValue(new SuccessResponse { IsSuccess = false });
                return res;
            }

            course.ProfessorId = profId;
            _courseRepository.Update(course);
            await _context.SaveChangesAsync();
            res.setValue(new SuccessResponse { IsSuccess = true });
            return res;
        }
    }
}
