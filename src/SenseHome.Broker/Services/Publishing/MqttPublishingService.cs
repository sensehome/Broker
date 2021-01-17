using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace SenseHome.Broker.Services.Publishing
{
    public class MqttPublishingService : IMqttPublishingService
    {
        private IMqttServer mqttServer;

        public void ConfigureMqttServer(IMqttServer mqttServer)
        {
            this.mqttServer = mqttServer;
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            options.WithApplicationMessageInterceptor(this);
        }

        public async Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            await Task.FromResult(context.AcceptPublish = true);
        }

        public Task InterceptClientMessageQueueEnqueueAsync(MqttClientMessageQueueInterceptorContext context)
        {
            return Task.FromResult(context.AcceptEnqueue);
        }
    }
}
