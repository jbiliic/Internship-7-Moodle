using Moodle.Domain.Entities;

namespace Moodle.Domain.Persistence.Repository
{
    public class ConversationRepository : Repository<Conversation, int>, IConversationRepository
    {
        public void Delete(Conversation id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Conversation>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Message>> GetAllMessagesAsync(int conversationId)
        {
            throw new NotImplementedException();
        }
    }
}
