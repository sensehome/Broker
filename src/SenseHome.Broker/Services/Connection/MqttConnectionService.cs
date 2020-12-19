using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;
using SenseHome.Broker.Services.Api;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.Authentication;

namespace SenseHome.Broker.Services.Connection
{
    public class MqttConnectionService : IMqttConnectionService
    {
        private IMqttServer mqttServer;
        private readonly IApiService apiService;

        public MqttConnectionService(IApiService apiService)
        {
            this.apiService = apiService;
        }


        public void ConfigureMqttServer(IMqttServer mqttServer)
        {
            this.mqttServer = mqttServer;
            mqttServer.ClientConnectedHandler = this;
            mqttServer.ClientDisconnectedHandler = this;
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            options.WithConnectionValidator(this);
        }

        public Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        public async Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            try
            {
                var loginDto = new UserLoginDto { Name = context.Username, Password = context.Password };
                var tokenDto = await apiService.LoginAsync(loginDto);
                var userDto = await apiService.GetUserProfileAsync(tokenDto);
                if(userDto.Id != context.ClientId)
                {
                    context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.ClientIdentifierNotValid;
                }
                else
                {
                    context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Success;
                }
            }
            catch(UnauthorizedException)
            {
                context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.BadUserNameOrPassword;
            }
        }
    }
}
