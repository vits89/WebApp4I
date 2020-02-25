using System.Web;
using Ninject.Activation;
using WebApp4I.Infrastructure.Services;
using WebApp4I.WebApp.Models;

namespace WebApp4I.WebApp.Infrastructure
{
    public class ImageFileProcessorProvider : Provider<ImageFileProcessor>
    {
        private readonly AppSettings _appSettings;

        public ImageFileProcessorProvider(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        protected override ImageFileProcessor CreateInstance(IContext context)
        {
            var path = HttpContext.Current.Server.MapPath("~" + _appSettings.PathToImages);

            return new ImageFileProcessor(path);
        }
    }
}
