using MQTTnet.Server;

namespace SenseHome.Broker.Services.Publishing
{
    public interface IMqttPublishingService : IMqttServerApplicationMessageInterceptor,
                                              IMqttServerClientMessageQueueInterceptor,
                                              IMqttConfigurationService
    {
    }
}
