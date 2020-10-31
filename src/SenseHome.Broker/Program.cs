using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MQTTnet.AspNetCore.Extensions;

namespace SenseHome.Broker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(option =>
                    {
                        option.ListenAnyIP(1883, listener => listener.UseMqtt());
                        option.ListenAnyIP(1884); // default HTTP Port
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
