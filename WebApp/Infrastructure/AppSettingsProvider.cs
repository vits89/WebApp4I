using System.Configuration;
using Ninject.Activation;
using WebApp4I.WebApp.Models;

namespace WebApp4I.WebApp.Infrastructure
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
