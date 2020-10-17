using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace SenseHome.Broker.Services.Server
{
    public class MqttServerService : IMqttServerService
    {
        public void ConfigureMqttServer(IMqttServer mqtt)
        {
            throw new System.NotImplementedException();
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            throw new System.NotImplementedException();
        }
    }
}
