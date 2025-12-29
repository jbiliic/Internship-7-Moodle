using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository.Common;

namespace Moodle.Domain.Persistence.Repository
{
    public interface IConversationRepository : IRepository<Conversation, int>
    {
        Task<IReadOnlyList<Message>> GetAllMessagesAsync(int conversationId);
        Task<Conversation?> GetConversationBetweenUsersAsync(int user1Id, int user2Id);
        void DeleteConversations(int userId);
    }
}
