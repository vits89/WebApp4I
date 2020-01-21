using AutoMapper;
using WebApp4I.Models;
using WebApp4I.ViewModels;

namespace WebApp4I.Infrastructure
{
    public class PathValueResolver : IMemberValueResolver<ImageInfo, ImageInfoViewModel, string, string>
    {
        private readonly AppSettings _appSettings;

        public PathValueResolver(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string Resolve(ImageInfo source, ImageInfoViewModel destination, string sourceMember, string destMember,
            ResolutionContext context)
        {
            return string.Concat(_appSettings.PathToImages, "/", sourceMember);
        }
    }
}
