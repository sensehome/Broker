using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace SenseHome.Broker.Services
{
    public interface IMqttConfigurationService
    {
        void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options);
        void ConfigureMqttServer(IMqttServer mqtt);
    }
}
