using System.Configuration;
using Ninject.Activation;
using WebApp4I.Models;

namespace WebApp4I.Infrastructure
{
    public class AppSettingsProvider : Provider<AppSettings>
    {
        protected override AppSettings CreateInstance(IContext context)
        {
            return new AppSettings
            {
                MapApiKey = ConfigurationManager.AppSettings["MapApiKey"],
                PathToImages = ConfigurationManager.AppSettings["PathToImages"]
            };
        }
    }
}
