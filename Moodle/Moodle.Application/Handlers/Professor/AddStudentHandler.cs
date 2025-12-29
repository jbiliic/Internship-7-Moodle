using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository;
using Moodle.Domain.Persistence.Repository.Common;

namespace Moodle.Application.Handlers.Professor
{
    public class AddStudentHandler
    {
        private readonly IRepository<IsEnrolled, int> _isEnrolledRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMoodleDbContext _context;
        public AddStudentHandler(IRepository<IsEnrolled, int> isEnrolledRepository,
            IMoodleDbContext context,
            ICourseRepository courseRepository)
        {
            _isEnrolledRepository = isEnrolledRepository;
            _context = context;
            _courseRepository = courseRepository;
        }
        public async Task<Result<SuccessResponse>> HandleAddStudentAsync(int userId, int courseId)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteAddStudentReq(userId, courseId, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteAddStudentReq(int userId, int courseId, Result<SuccessResponse> res)
        {
            var isEnrolled = await _courseRepository.IsStudentEnrolledAsync(userId, courseId);
            if (isEnrolled)
            {
                res.setValue(new SuccessResponse { IsSuccess = false});
                var validationResult = new ValidationResult();
                validationResult.AddValidationItem(ValidationItems.IsEnrolled.StudentAlreadyEnrolled);
                res.setValidationResult(validationResult);
                return res;
            }
            var enrollment = new IsEnrolled
            {
                UserId = userId,
                CourseId = courseId
            };
            await _isEnrolledRepository.InsertAsync(enrollment);
            await _context.SaveChangesAsync();
            res.setValue(new SuccessResponse { IsSuccess = true, Id = enrollment.Id });
            return res;
        }
    }
}
