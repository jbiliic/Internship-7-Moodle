using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository.Common;

namespace Moodle.Domain.Persistence.Repository
{
    public interface IConversationRepository
    {
        Task<IReadOnlyList<Message>> GetAllMessagesAsync(int conversationId);
    }
}
