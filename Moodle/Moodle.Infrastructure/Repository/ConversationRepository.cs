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
    }
}
