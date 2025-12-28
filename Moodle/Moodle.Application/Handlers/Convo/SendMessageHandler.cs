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
        public async Task<Result<SuccessResponse<Message>>> HandleSendMessageReq(MessageDTO messageDTO , int conversationId)
        {
            var res = new Result<SuccessResponse<Message>>();
            return await ExecuteSendMessageReq(messageDTO,conversationId, res);
        }
        private async Task<Result<SuccessResponse<Message>>> ExecuteSendMessageReq(MessageDTO messageDTO, int conversationId, Result<SuccessResponse<Message>> res) { 
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
                res.setValue(new SuccessResponse<Message> { IsSuccess = false, Item = null });
                return res;
            }
            await _messageRepository.InsertAsync(mess);
            await _context.SaveChangesAsync();
            res.setValue(new SuccessResponse<Message> { IsSuccess = true, Item = mess , Id = mess.Id });
            return res;
        }
    }
}
