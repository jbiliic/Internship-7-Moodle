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

        public async Task<Result<SuccessResponse<CourseNotifDTO>>> HandleAddNotificationAsync(CourseNotifDTO notificationDTO , int courseId , int profId)
        {
            var res = new Result<SuccessResponse<CourseNotifDTO>>();
            return await ExecuteAddNotificationAsync(notificationDTO,courseId,profId, res);
        }
        private async Task<Result<SuccessResponse<CourseNotifDTO>>> ExecuteAddNotificationAsync(CourseNotifDTO notificationDTO,int courseId,int profId, Result<SuccessResponse<CourseNotifDTO>> res)
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
                res.setValue(new SuccessResponse<CourseNotifDTO>() { IsSuccess = false , Item = null});
                return res;
            }

            await _notificationRepo.InsertAsync(notif);
            await _context.SaveChangesAsync();

            res.setValue(new SuccessResponse<CourseNotifDTO>() {IsSuccess = true , Id = notif.Id , Item = notificationDTO });
            return res;
        }

        public async Task<Result<SuccessResponse<MaterialsDTO>>> HandleAddMatsAsync(MaterialsDTO matsDTO, int courseId, int profId)
        {
            var res = new Result<SuccessResponse<MaterialsDTO>>();
            return await ExecuteAddMatsAsync(matsDTO, courseId, profId, res);
        }
        private async Task<Result<SuccessResponse<MaterialsDTO>>> ExecuteAddMatsAsync(MaterialsDTO matsDTO, int courseId, int profId, Result<SuccessResponse<MaterialsDTO>> res)
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
                res.setValue(new SuccessResponse<MaterialsDTO>() { IsSuccess = false, Item = null });
                return res;
            }

            await _materialRepo.InsertAsync(mats);
            await _context.SaveChangesAsync();

            res.setValue(new SuccessResponse<MaterialsDTO>() { IsSuccess = true, Id = mats.Id, Item = matsDTO });
            return res;
        }
    }
}

