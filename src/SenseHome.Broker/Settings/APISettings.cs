using System;
namespace SenseHome.Broker.Settings
{
    public class ApiSettings
    {
        public string Host { get; set; }
        public string UserLoginRoute { get; set; }
        public string UserProfileRoute { get; set; }
        public string UserSubscriptionRoute { get; set; }
    }
}
