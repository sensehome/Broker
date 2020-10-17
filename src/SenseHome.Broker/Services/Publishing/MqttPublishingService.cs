using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace SenseHome.Broker.Services.Publishing
{
    public class MqttPublishingService : IMqttPublishingService
    {
        public void ConfigureMqttServer(IMqttServer mqtt)
        {
            throw new System.NotImplementedException();
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            throw new System.NotImplementedException();
        }

        public Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task InterceptClientMessageQueueEnqueueAsync(MqttClientMessageQueueInterceptorContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
