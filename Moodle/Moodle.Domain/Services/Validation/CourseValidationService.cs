using Moodle.Domain.Common.Validation.ValidationItems;
using Moodle.Domain.Entities.Course;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Domain.Services.Validation
{
    public class CourseValidationService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseValidationService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Common.Validation.ValidationResult> ValidateAsync(Course course)
        {
            var res = course.ValidateBasic();
            var existingCourse = await _courseRepository.GetByNameAndMajorAsync(course.Name, course.Major);
            if (existingCourse is not null)
            {
                res.AddValidationItem(
                    ValidationItems.Course.CourseExists
                    );
            }
            return res;
        }
    }
}
