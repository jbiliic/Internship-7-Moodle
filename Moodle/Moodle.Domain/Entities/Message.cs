using Moodle.Domain.Common.Entities;
using Moodle.Domain.Common.Validation;
using Moodle.Domain.Common.Validation.ValidationItems;

namespace Moodle.Domain.Entities
{
    public class Message : BaseEntity
    {
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Conversation Conversation { get; set; } = null!;
        public User Sender { get; set; } = null!;

        ValidationResult Validate() { 
            var resault = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Title))
            {
                resault.AddValidationItem(
                    ValidationItems.Message.EmptyTitleErr
                    );
            }
            if (string.IsNullOrWhiteSpace(Content))
            {
                resault.AddValidationItem(
                    ValidationItems.Message.EmptyContentErr
                    );
            }
            if(Content.Length + Title.Length > MessageMaxLen)
            {                 
                resault.AddValidationItem(
                    ValidationItems.Message.ExceedMaxLengthErr
                    );
            }
            return resault;
        }
    }
}
