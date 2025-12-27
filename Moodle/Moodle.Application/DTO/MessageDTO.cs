using Moodle.Domain.Entities;

namespace Moodle.Application.DTO
{
    public class MessageDTO
    {
        public int SenderId { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.Now;

        public MessageDTO(Message mes)
        {
            SenderId = mes.SenderId;
            Title = mes.Title;
            Content = mes.Content;
            SentAt = mes.CreatedAt;
        }
        public MessageDTO(int senderId , string? title , string? content)
        {
            SenderId = senderId;
            Title = title;
            Content = content;
        }
    }
}
