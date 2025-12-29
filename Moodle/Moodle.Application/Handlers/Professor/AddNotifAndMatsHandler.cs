using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Entities.Course;
using Moodle.Domain.Persistence.Repository.Common;

namespace Moodle.Application.Handlers.Professor
{
    public class AddNotifAndMatsHandler
    {
        private readonly IRepository<CourseNotification, int> _notificationRepo;
        private readonly IRepository<LearningMaterials, int> _materialRepo;
        private readonly IMoodleDbContext _context;

        public AddNotifAndMatsHandler(IRepository<CourseNotification, int> notificationRepo,
            IRepository<LearningMaterials, int> materialRepo,
            IMoodleDbContext context)
        {
            _notificationRepo = notificationRepo;
            _materialRepo = materialRepo;
            _context = context;
        }

        public async Task<Result<SuccessResponse>> HandleAddNotificationAsync(CourseNotifDTO notificationDTO , int courseId , int profId)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteAddNotificationAsync(notificationDTO,courseId,profId, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteAddNotificationAsync(CourseNotifDTO notificationDTO,int courseId,int profId, Result<SuccessResponse> res)
        {
            var notif = new CourseNotification()
            {
                Title = notificationDTO.Title,
                Content = notificationDTO.Content,
                ProfessorId = profId,
                CourseId = courseId
            };

            var validationRes = notif.Validate();
            if (validationRes.HasErrors) {
                res.setValidationResult(validationRes);
                res.setValue(new SuccessResponse() { IsSuccess = false });
                return res;
            }

            await _notificationRepo.InsertAsync(notif);
            await _context.SaveChangesAsync();

            res.setValue(new SuccessResponse() {IsSuccess = true , Id = notif.Id});
            return res;
        }

        public async Task<Result<SuccessResponse>> HandleAddMatsAsync(MaterialsDTO matsDTO, int courseId, int profId)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteAddMatsAsync(matsDTO, courseId, profId, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteAddMatsAsync(MaterialsDTO matsDTO, int courseId, int profId, Result<SuccessResponse> res)
        {
            var mats = new LearningMaterials()
            {
                Title = matsDTO.Title,
                FilePath = matsDTO.FilePath,
                UploaderId = profId,
                CourseId = courseId
            };

            var validationRes = mats.Validate();
            if (validationRes.HasErrors)
            {
                res.setValidationResult(validationRes);
                res.setValue(new SuccessResponse() { IsSuccess = false});
                return res;
            }

            await _materialRepo.InsertAsync(mats);
            await _context.SaveChangesAsync();

            res.setValue(new SuccessResponse() { IsSuccess = true, Id = mats.Id });
            return res;
        }
    }
}

