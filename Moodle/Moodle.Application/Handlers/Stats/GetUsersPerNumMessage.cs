using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;

namespace Moodle.Application.Handlers.Stats
{
    public class GetUsersPerNumMessage
    {
        private readonly IUserRepository _userRepository;
        public GetUsersPerNumMessage(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<GetAllResponse<UserNumMessagesDTO>>> HandleGetUsersPerNumMessageAsync(DateTimeOffset date)
        {
            var res = new Result<GetAllResponse<UserNumMessagesDTO>>();
            return await ExecuteGetUsersPerNumMessageAsync(res, date);
        }
        private async Task<Result<GetAllResponse<UserNumMessagesDTO>>> ExecuteGetUsersPerNumMessageAsync(Result<GetAllResponse<UserNumMessagesDTO>> res, DateTimeOffset date)
        {
            date = date.ToUniversalTime();
            var users = await _userRepository.GetTop3MostMessagesAsync(date);
            if (users == null || !users.Any())
            {
                res.setValue(new GetAllResponse<UserNumMessagesDTO> { Items = new List<UserNumMessagesDTO>() });
                return res;
            }
            var toReturn = new List<UserNumMessagesDTO>();
            foreach (var user in users) {
                var number = await _userRepository.GetNumMessagesPerUser(user.Id, date);
                toReturn.Add(new UserNumMessagesDTO { userId = user.Id, numMessages = number , email = user.Email });
            }
            res.setValue(new GetAllResponse<UserNumMessagesDTO> { Items = toReturn });
            return res;
        }
    }
}
