using Moodle.Application.Common;
using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Entities;
using Moodle.Domain.Persistence.Repository.Common;

namespace Moodle.Application.Handlers.Convo
{
    public class SendMessageHandler
    {
        private readonly IRepository<Message,int> _messageRepository;
        private readonly IMoodleDbContext _context;
        public SendMessageHandler(IRepository<Message, int> messageRepository , IMoodleDbContext context)
        {
            _messageRepository = messageRepository;
            _context = context;
        }
        public async Task<Result<SuccessResponse>> HandleSendMessageReq(MessageDTO messageDTO , int conversationId)
        {
            var res = new Result<SuccessResponse>();
            return await ExecuteSendMessageReq(messageDTO,conversationId, res);
        }
        private async Task<Result<SuccessResponse>> ExecuteSendMessageReq(MessageDTO messageDTO, int conversationId, Result<SuccessResponse> res) { 
            var mess = new Message
            {
                ConversationId = conversationId,
                SenderId = messageDTO.SenderId,
                Content = messageDTO.Content,
                Title = messageDTO.Title
            };
            var validationRes = mess.Validate();
            if (validationRes.HasErrors)
            {
                res.setValidationResult(validationRes);
                res.setValue(new SuccessResponse { IsSuccess = false });
                return res;
            }
            await _messageRepository.InsertAsync(mess);
            await _context.SaveChangesAsync();
            res.setValue(new SuccessResponse{ IsSuccess = true, Id = mess.Id });
            return res;
        }
    }
}
