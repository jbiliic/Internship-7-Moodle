using Moodle.Domain.Entities.Course;

namespace Moodle.Application.DTO
{
    public class MaterialsDTO
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public DateTimeOffset UploadedAt { get; set; }
        public MaterialsDTO(string title, string filePath, DateTimeOffset uploadedAt)
        {
            Title = title;
            FilePath = filePath;
            UploadedAt = uploadedAt;
        }
        public MaterialsDTO(LearningMaterials mats)
        {
            Title = mats.Title;
            FilePath = mats.FilePath;
            UploadedAt = mats.CreatedAt;
        }
    }
}
