using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Application.Handlers;
using Moodle.Domain.Entities;

namespace Moodle.Presentation.Menus
{
    internal class ChatMenu  : IMenu
    {
        private  readonly GetUserChatsHandler _userChatHandler;
        private  readonly GetChatAndMessagesHandler _chatAndMessagesHandler;
        private readonly SendMessageHandler _sendMessageHandler;

        public ChatMenu(GetUserChatsHandler userChatHandler, GetChatAndMessagesHandler chatAndMessagesHandler , SendMessageHandler messageHandler) { 
            _userChatHandler = userChatHandler;
            _chatAndMessagesHandler = chatAndMessagesHandler;
            _sendMessageHandler = messageHandler;
        }
        public async Task Show(UserDTO currUser)
        {
            while (true) { 
                Console.Clear();
                Console.WriteLine("Private Chat Menu");
                Console.WriteLine("1. View Chats");
                Console.WriteLine("2. Start New Chat");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Select an option: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var res = await _userChatHandler.HandleGetChats(currUser.Id);
                        await ViewChats(currUser, res);
                        break;
                    case '2':
                        var res2 = await _userChatHandler.HandleGetWOChats(currUser.Id);
                        await ViewChats(currUser, res2);
                        break;
                    case '0':
                        return;
                    default:
                        Helper.Helper.clearDisplAndDisplMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }
        public async Task ViewChats(UserDTO currUser , Resault<GetAllResponse<UserDTO>> res)
        {
            while (true)
            {
                Console.Clear();
                if (!res.Value.IsSuccess)
                {
                    Helper.Helper.clearDisplAndDisplMessage("Empty...");
                    return;
                }
                var input = -1;
                while (true)
                {
                    Console.WriteLine("Options:");
                    var number = 1;
                    foreach (var chat in res.Value.Items)
                    {
                        Console.WriteLine($"ChatNum:{number}. , Participant: {(!string.IsNullOrWhiteSpace(chat.FirstName) ? chat.FirstName : chat.Email)}");
                        number++;
                    }
                    input = Helper.Helper.getAndValidateInputInt("chat number to open or '0' to go back");
                    if (input == 0) return;
                    if (input < 1 || input >= number)
                    {
                        Helper.Helper.clearDisplAndDisplMessage("Invalid chat number. Please try again.");
                        continue;
                    }
                    break;
                }
                var otherUserId = res.Value.Items[input - 1].Id;
                var resC = await _chatAndMessagesHandler.HandleGetConversationReq(currUser.Id, otherUserId);
                var conversation = resC.Value.Item;
                await EnterChat(conversation, currUser);
            }
        }

        public async Task EnterChat(Conversation conversation , UserDTO currUser)
        {
            var otherUserId = conversation.User1Id == currUser.Id ? conversation.User2Id : conversation.User1Id;
            var res = await _chatAndMessagesHandler.HandleGetMessagesReq(conversation.Id);
            var messages = new List<MessageDTO>(res.Value.Items);
            
            while (true)
            {
                Console.Clear();
                Helper.Helper.renderMessages(messages, otherUserId);
                var title = Helper.Helper.getStringOptional("title to send (or /exit to go back)");
                if (title == "/exit")
                    return;
                var content = Helper.Helper.getStringOptional("content");
                var messageDto = new MessageDTO
                (
                     currUser.Id,
                     title,
                     content
                );
                var sendRes = await _sendMessageHandler.HandleSendMessageReq(messageDto , conversation.Id);

                if (sendRes.hasErrors) { 
                    Helper.Helper.displayValidationErrors(sendRes.Errors);
                    continue;
                }
                messages.Add(messageDto);
            }
        }
    }
}
