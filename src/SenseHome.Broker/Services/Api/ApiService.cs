using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SenseHome.Broker.Settings;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.Authentication;
using SenseHome.DataTransferObjects.Error;
using SenseHome.DataTransferObjects.Subscription;
using SenseHome.DataTransferObjects.User;

namespace SenseHome.Broker.Services.Api
{
    public class ApiService : IApiService
    {
        private readonly ApiSettings settings;

        public ApiService(ApiSettings settings)
        {
            this.settings = settings;
        }

        public async Task<UserDto> GetUserProfileAsync(TokenDto tokenDto)
        {
            var httpClient = new HttpClient();
            var profileEndpoint = $"{settings.Host}/{settings.UserProfileRoute}";
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenDto.Bearer}");
            var response = await httpClient.GetAsync(profileEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var userDto = JsonConvert.DeserializeObject<UserDto>(responseString);
                return userDto;
            }
            else
            {
                var responseString = await response?.Content?.ReadAsStringAsync();
                var errorDto = JsonConvert.DeserializeObject<ExceptionDetailsDto>(responseString);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedException(errorDto.Message);
                    default:
                        throw new System.Exception(errorDto.Message);
                }
            }
        }

        public async Task<SubscriptionDto> GetUserSubscriptionsAsync(string userId, TokenDto token)
        {
            var httpClient = new HttpClient();
            var subscriptionRoute = settings.UserSubscriptionRoute.Replace("{id}", userId);
            var loginEndpoint = $"{settings.Host}/{subscriptionRoute}";
            httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {token.Bearer}");
            var response = await httpClient.GetAsync(loginEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var subscription = JsonConvert.DeserializeObject<SubscriptionDto>(responseString);
                return subscription;
            }
            else
            {
                var responseString = await response?.Content?.ReadAsStringAsync();
                var errorDto = JsonConvert.DeserializeObject<ExceptionDetailsDto>(responseString);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedException(errorDto.Message);
                    case System.Net.HttpStatusCode.NotFound:
                        throw new NotFoundException(errorDto.Message);
                    default:
                        throw new System.Exception(errorDto.Message);
                }
            }
        }

        public async Task<TokenDto> LoginAsync(UserLoginDto loginDto)
        {
            var httpClient = new HttpClient();
            var body = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var loginEndpoint = $"{settings.Host}/{settings.UserLoginRoute}";
            var response = await httpClient.PostAsync(loginEndpoint, body);
            if(response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var tokenDto = JsonConvert.DeserializeObject<TokenDto>(responseString);
                return tokenDto;
            }
            else
            {
                var responseString = await response?.Content?.ReadAsStringAsync();
                var errorDto = JsonConvert.DeserializeObject<ExceptionDetailsDto>(responseString);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedException(errorDto.Message);
                    case System.Net.HttpStatusCode.NotFound:
                        throw new NotFoundException(errorDto.Message);
                    default:
                        throw new System.Exception(errorDto.Message);
                }
            }
        }
    }
}
