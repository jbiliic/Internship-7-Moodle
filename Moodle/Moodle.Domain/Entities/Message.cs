using Moodle.Domain.Common.Entities;

namespace Moodle.Domain.Entities
{
    public class Message : BaseEntity
    {
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        Conversation Conversation { get; set; } = null!;
        public User Sender { get; set; } = null!;
    }
}
