using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Convo
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

        public async Task<Result<SuccessResponseGet<Conversation>>> HandleGetConversationReq(int user1, int user2)
        {
            var res = new Result<SuccessResponseGet<Conversation>>();
            if (user1 > user2)
                return await ExecuteGetConversationReq(user2, user1, res);
            return await ExecuteGetConversationReq(user1, user2, res);
        }
        private async Task<Result<SuccessResponseGet<Conversation>>> ExecuteGetConversationReq(int user1, int user2, Result<SuccessResponseGet<Conversation>> res)
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
            res.setValue(new SuccessResponseGet<Conversation> { Item = conversation });
            return res;
        }


        public async Task<Result<GetAllResponse<MessageDTO>>> HandleGetMessagesReq(int convoId)
        {
            var res = new Result<GetAllResponse<MessageDTO>>();
            return await ExecuteMessagesReq(convoId, res);
        }
        private async Task<Result<GetAllResponse<MessageDTO>>> ExecuteMessagesReq(int convoId, Result<GetAllResponse<MessageDTO>> res)
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
