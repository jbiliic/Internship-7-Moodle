using Moodle.Application.Common.Model;
using Moodle.Application.DTO;
using Moodle.Domain.Persistence.Repository;
using Moodle.Domain.Services.Cache.Common;

namespace Moodle.Application.Handlers.Stats
{
    public class GetUsersPerNumMessage
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService<UserNumMessagesDTO> _cacheService;
        public GetUsersPerNumMessage(IUserRepository userRepository, ICacheService<UserNumMessagesDTO> cacheService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
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
                var cacheKey = $"UserNumMessagesDTO_{user.Id}";
                var cachedData = await _cacheService.GetAsync(cacheKey);
                if (cachedData != null)
                {
                    toReturn.Add(cachedData);
                    continue;
                }
                var number = await _userRepository.GetNumMessagesPerUser(user.Id, date);
                toReturn.Add(new UserNumMessagesDTO { userId = user.Id, numMessages = number , email = user.Email });
                await _cacheService.SetAsync(cacheKey, toReturn.Last(), TimeSpan.FromHours(1));
            }
            res.setValue(new GetAllResponse<UserNumMessagesDTO> { Items = toReturn });
            return res;
        }
    }
}
