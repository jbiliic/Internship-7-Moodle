using Moodle.Domain.Entities.Course;

namespace Moodle.Application.DTO
{
    public class CourseNotifDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ProfessorEmail { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public CourseNotifDTO(string title, string content,DateTimeOffset createdAt)
        {
            Title = title;
            Content = content;
            CreatedAt = createdAt;
        }
        public CourseNotifDTO(CourseNotification courseNotification)
        {
            Title = courseNotification.Title;
            Content = courseNotification.Content;
            CreatedAt = courseNotification.CreatedAt;
            ProfessorEmail = courseNotification.Professor.Email;
        }
    }
}
