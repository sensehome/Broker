﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.Extensions;
using SenseHome.Broker.Services.Connection;
using SenseHome.Broker.Services.Publishing;
using SenseHome.Broker.Services.Server;
using SenseHome.Broker.Services.Subscription;

namespace SenseHome.Broker.Configuration
{
    public static class MqttConfiguration
    {
        public static void AddConfiguredMqttServices(this IServiceCollection services)
        {
            services.AddSingleton<IMqttConnectionService, MqttConnectionService>();
            services.AddSingleton<IMqttSubscriptionService, MqttSubscriptionService>();
            services.AddSingleton<IMqttPublishingService, MqttPublishingService>();
            services.AddSingleton<MqttServerService>();

            services.AddHostedMqttServerWithServices(options =>
            {
                var mqttService = options.ServiceProvider.GetRequiredService<MqttServerService>();
                mqttService.ConfigureMqttServerOptions(options);
            });
            services.AddMqttConnectionHandler();
            services.AddMqttWebSocketServerAdapter();
            services.AddConnections();
        }

        public static void UseConfiguredMqttServer(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapMqtt("/mqtt");
            });
            app.UseMqttServer(mqttServer =>
            {
                app.ApplicationServices.GetRequiredService<MqttServerService>().ConfigureMqttServer(mqttServer);
            });
        }
    }
}
