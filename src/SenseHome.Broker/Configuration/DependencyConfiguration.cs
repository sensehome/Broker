using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenseHome.Broker.Services.Api;
using SenseHome.Broker.Settings;
using SenseHome.Broker.Utility;

namespace SenseHome.Broker.Configuration
{
    public static class DependencyConfiguration
    {
        public static void AddDependentServicesAndSettings(this IServiceCollection services, IConfiguration configuration)
        {
            ApiSettings apiSettings = new ApiSettings();
            configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);
            services.AddSingleton(apiSettings);
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<BrokerEventTopics>();
            services.AddSingleton<BrokerCommandTopics>();
        }
    }
}
