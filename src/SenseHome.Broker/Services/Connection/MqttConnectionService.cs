

using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace SenseHome.Broker.Services.Connection
{
    public class MqttConnectionService : IMqttConnectionService
    {
        private IMqttServer mqttServer;

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
            await Task.FromResult(context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Success);
        }
    }
}
