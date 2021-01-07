using System.Threading.Tasks;
using MQTTnet.Server;

namespace SenseHome.Broker.Services.Internal
{
    public interface IMqttInternalService
    {
        Task ServeConnectedClientsAsync();
        Task ServeConnectedClientsCountAsync();
        Task ServeClientIPAsync(string clientId);
        Task ServeClientConnectedTimeAsync(string clientId);
        Task ExecuteSystemCommandAsync(MqttApplicationMessageInterceptorContext context);
    }

}
