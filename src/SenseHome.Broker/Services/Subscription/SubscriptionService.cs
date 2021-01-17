using System;
using System.Threading.Tasks;
using MQTTnet.AspNetCore;
using MQTTnet.Server;
using SenseHome.Broker.Services.Api;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.Authentication;

namespace SenseHome.Broker.Services.Subscription
{
    public class MqttSubscriptionService : IMqttSubscriptionService
    {
        private IMqttServer mqttServer;
        private readonly IApiService apiService;

        public MqttSubscriptionService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public void ConfigureMqttServer(IMqttServer mqttServer)
        {
            this.mqttServer = mqttServer;
            mqttServer.ClientSubscribedTopicHandler = this;
            mqttServer.ClientUnsubscribedTopicHandler = this;
        }

        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            options.WithSubscriptionInterceptor(this);
            options.WithUnsubscriptionInterceptor(this);
        }

        public Task HandleClientSubscribedTopicAsync(MqttServerClientSubscribedTopicEventArgs eventArgs)
        {
            return Task.CompletedTask;
        }

        public Task HandleClientUnsubscribedTopicAsync(MqttServerClientUnsubscribedTopicEventArgs eventArgs)
        {
            return Task.CompletedTask;
        }

        public async Task InterceptSubscriptionAsync(MqttSubscriptionInterceptorContext context)
        {
            object bearer;
            context.SessionItems.TryGetValue(nameof(bearer), out bearer);
            if(bearer == null)
            {
                context.AcceptSubscription = false;
                context.CloseConnection = true;
                return;
            }
            var tokenDto = new TokenDto { Bearer = bearer.ToString() };
            try
            {
                var subscription = await apiService.GetUserSubscriptionsAsync(context.ClientId, tokenDto);
                foreach (var path in subscription.Path)
                {
                    if (MqttTopicFilterComparer.IsMatch(context.TopicFilter.Topic, path))
                    {
                        context.AcceptSubscription = true;
                        return;
                    }
                }
                context.AcceptSubscription = false;
            }
            catch(UnauthorizedException)
            {
                context.AcceptSubscription = false;
                context.CloseConnection = true;
            }
            catch(Exception)
            {
                context.AcceptSubscription = false;
            } 
        }

        public async Task InterceptUnsubscriptionAsync(MqttUnsubscriptionInterceptorContext context)
        {
            await Task.FromResult(context.CloseConnection = false);
        }
    }
}
