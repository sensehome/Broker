using MQTTnet.Server;

namespace SenseHome.Broker.Services.Connection
{
    public interface IMqttConnectionService : IMqttServerConnectionValidator,
                                              IMqttServerClientConnectedHandler,
                                              IMqttServerClientDisconnectedHandler,
                                              IMqttConfigurationService
    {
    }
}
