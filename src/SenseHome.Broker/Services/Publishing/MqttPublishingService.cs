using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;
using SenseHome.Broker.Services.Internal;
using SenseHome.Broker.Utility;

namespace SenseHome.Broker.Services.Publishing
{
    public class MqttPublishingService : IMqttPublishingService
    {
        private IMqttServer mqttServer;
        private IMqttInternalService internalService;
        private readonly BrokerCommandTopics brokerCommandTopics;
        private readonly BrokerEventTopics brokerEventTopics;

        public MqttPublishingService(BrokerCommandTopics brokerCommandTopics, BrokerEventTopics brokerEventTopics)
        {
            this.brokerCommandTopics = brokerCommandTopics;
            this.brokerEventTopics = brokerEventTopics;
        }

        public void ConfigureMqttServer(IMqttServer mqttServer)
        {
            this.mqttServer = mqttServer;
            this.internalService = new MqttInternalService(mqttServer, brokerCommandTopics, brokerEventTopics);
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            options.WithApplicationMessageInterceptor(this);
        }

        public async Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            if (IsSystemTopic(context.ApplicationMessage.Topic))
            {
                if (IsBrokerItself(context.ClientId))
                {
                    context.AcceptPublish = true;
                }
                else
                {
                    await internalService.ExecuteSystemCommandAsync(context);
                    context.AcceptPublish = false;
                    return;
                }
            }
            else
            {
                context.AcceptPublish = true;
            }
        }

        public Task InterceptClientMessageQueueEnqueueAsync(MqttClientMessageQueueInterceptorContext context)
        {
            return Task.FromResult(context.AcceptEnqueue);
        }

        #region private methods
        private bool IsSystemTopic(string topic)
        {
            return topic.StartsWith("$SYS");
        }

        private bool IsBrokerItself(string clientId)
        {
            return string.IsNullOrEmpty(clientId);
        }
        #endregion
    }
}
