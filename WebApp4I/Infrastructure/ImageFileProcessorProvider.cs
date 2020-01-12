using System.Web;
using Ninject.Activation;
using WebApp4I.Models;

namespace WebApp4I.Infrastructure
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
