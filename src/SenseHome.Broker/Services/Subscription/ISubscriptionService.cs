using MQTTnet.Server;

namespace SenseHome.Broker.Services.Subscription
{
    public interface IMqttSubscriptionService : IMqttServerClientSubscribedTopicHandler,
                                                IMqttServerSubscriptionInterceptor,
                                                IMqttServerClientUnsubscribedTopicHandler,
                                                IMqttServerUnsubscriptionInterceptor,
                                                IMqttConfigurationService
    {
    }
}
