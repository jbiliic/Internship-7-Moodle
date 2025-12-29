using Microsoft.EntityFrameworkCore;
using Moodle.Domain.Entities;
using Moodle.Infrastructure.Database;
using Moodle.Infrastructure.Repository.Common;

namespace Moodle.Domain.Persistence.Repository
{
    public class ConversationRepository : Repository<Conversation, int>, IConversationRepository
    {
        private readonly MoodleDbContext _context;
        public ConversationRepository(MoodleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Message>> GetAllMessagesAsync(int conversationId)
        {
            return await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .ToListAsync();
        }
        public async Task<Conversation?> GetConversationBetweenUsersAsync(int user1Id, int user2Id)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c => 
                    (c.User1Id == user1Id && c.User2Id == user2Id));
        }
        public void DeleteConversations(int userId)
        {
            _context.Conversations
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .ExecuteDelete();
        }
    }
}
