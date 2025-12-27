using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers
{
    public class GetChatAndMessagesHandler
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMoodleDbContext _context;
        public GetChatAndMessagesHandler(IConversationRepository conversationRepository , IMoodleDbContext context)
        {
            _conversationRepository = conversationRepository;
            _context = context;
        }

        public async Task<Resault<SuccessResponse<Conversation>>> HandleGetConversationReq(int user1, int user2)
        {
            var res = new Resault<SuccessResponse<Conversation>>();
            if (user1 > user2)
                return await ExecuteGetConversationReq(user2, user1, res);
            return await ExecuteGetConversationReq(user1, user2, res);
        }
        private async Task<Resault<SuccessResponse<Conversation>>> ExecuteGetConversationReq(int user1, int user2, Resault<SuccessResponse<Conversation>> res)
        {
            var conversation = await _conversationRepository.GetConversationBetweenUsersAsync(user1, user2);
            if (conversation == null)
            {
                conversation = new Conversation
                {
                    User1Id = user1,
                    User2Id = user2
                };
                await _conversationRepository.InsertAsync(conversation);
                await _context.SaveChangesAsync();
            }
            res.setValue(new SuccessResponse<Conversation> { Item = conversation });
            return res;
        }


        public async Task<Resault<GetAllResponse<MessageDTO>>> HandleGetMessagesReq(int convoId)
        {
            var res = new Resault<GetAllResponse<MessageDTO>>();
            return await ExecuteMessagesReq(convoId, res);
        }
        private async Task<Resault<GetAllResponse<MessageDTO>>> ExecuteMessagesReq(int convoId, Resault<GetAllResponse<MessageDTO>> res)
        {
            var messages = await _conversationRepository.GetAllMessagesAsync(convoId);
            if (messages == null || messages.Count == 0)
            {
                res.setValue(new GetAllResponse<MessageDTO> { Items = new List<MessageDTO>() });
                return res;
            }
            var messageDtos = messages.Select(m =>  new MessageDTO(m)).ToList();
            res.setValue(new GetAllResponse<MessageDTO> { Items = messageDtos });
            return res;
        }

    }
}
