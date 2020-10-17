

using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace SenseHome.Broker.Services.Connection
{
    public class MqttConnectionService : IMqttConnectionService
    {
        public void ConfigureMqttServer(IMqttServer mqtt)
        {
            throw new System.NotImplementedException();
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            throw new System.NotImplementedException();
        }

        public Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
