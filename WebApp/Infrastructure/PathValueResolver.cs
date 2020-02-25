using AutoMapper;
using WebApp4I.WebApp.Models;

namespace WebApp4I.WebApp.Infrastructure
{
    public class PathValueResolver : IMemberValueResolver<object, object, string, string>
    {
        private readonly AppSettings _appSettings;

        public PathValueResolver(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string Resolve(object source, object destination, string sourceMember, string destMember,
            ResolutionContext context)
        {
            return string.Concat(_appSettings.PathToImages, "/", sourceMember);
        }
    }
}
