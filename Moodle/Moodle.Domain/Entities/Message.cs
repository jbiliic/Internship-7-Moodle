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

        public ValidationResult Validate() { 
            var Result = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Title))
            {
                Result.AddValidationItem(
                    ValidationItems.Message.EmptyTitleErr
                    );
            }
            if (string.IsNullOrWhiteSpace(Content))
            {
                Result.AddValidationItem(
                    ValidationItems.Message.EmptyContentErr
                    );
            }
            if(Content.Length + Title.Length > MessageMaxLen)
            {                 
                Result.AddValidationItem(
                    ValidationItems.Message.ExceedMaxLengthErr
                    );
            }
            return Result;
        }
    }
}
