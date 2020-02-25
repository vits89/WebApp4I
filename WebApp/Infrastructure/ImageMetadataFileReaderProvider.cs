using System.Web;
using Ninject.Activation;
using WebApp4I.Infrastructure.Services;
using WebApp4I.WebApp.Models;

namespace WebApp4I.WebApp.Infrastructure
{
    public class ImageMetadataFileReaderProvider : Provider<ImageMetadataFileReader>
    {
        private readonly AppSettings _appSettings;

        public ImageMetadataFileReaderProvider(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        protected override ImageMetadataFileReader CreateInstance(IContext context)
        {
            var path = HttpContext.Current.Server.MapPath("~" + _appSettings.PathToImages);

            return new ImageMetadataFileReader(path);
        }
    }
}
