using System.Web;
using Ninject.Activation;
using WebApp4I.Models;

namespace WebApp4I.Infrastructure
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
