using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SenseHome.Broker.Settings;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.Authentication;
using SenseHome.DataTransferObjects.Error;
using SenseHome.DataTransferObjects.User;

namespace SenseHome.Broker.Services.Api
{
    public class ApiService : IApiService
    {
        private readonly APISettings settings;

        public ApiService(APISettings settings)
        {
            this.settings = settings;
        }

        public async Task<UserDto> GetUserProfileAsync(TokenDto tokenDto)
        {
            var httpClient = new HttpClient();
            var loginEndpoint = $"{settings.Host}/{settings.UserLoginRoute}";
            httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {tokenDto.Bearer}");
            var response = await httpClient.GetAsync(loginEndpoint);
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
                throw new UnauthorizedException(errorDto.Message);
            }
        }

        public async Task<TokenDto> LoginAsync(UserLoginDto loginDto)
        {
            var httpClient = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "name", loginDto.Name },
                { "password", loginDto.Password }
            };
            var body = new FormUrlEncodedContent(values);
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
                throw new UnauthorizedException(errorDto.Message);
            }
        }
    }
}
