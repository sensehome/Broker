using System.Collections.Generic;
using System.Threading.Tasks;
using SenseHome.DataTransferObjects.Authentication;
using SenseHome.DataTransferObjects.Subscription;
using SenseHome.DataTransferObjects.User;

namespace SenseHome.Broker.Services.Api
{
    public interface IApiService
    {
        Task<TokenDto> LoginAsync(UserLoginDto loginDto);
        Task<UserDto> GetUserProfileAsync(TokenDto tokenDto);
        Task<IEnumerable<SubscriptionDto>> GetUserSubscriptionsAsync(string userId, TokenDto token);
    }
}
