using AutoMapper;
using WebApp4I.AppCore.Entities;
using WebApp4I.WebApp.Infrastructure;
using WebApp4I.WebApp.ViewModels;

namespace WebApp4I.WebApp
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configuration { get; private set; }

        public static void Initialize()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageInfo, ImageInfoViewModel>()
                    .ForMember(dest => dest.Path, opt => opt.MapFrom<PathValueResolver, string>(src => src.FileName));
                cfg.CreateMap<ImageInfo, ImageThumbnailInfoViewModel>()
                    .ForMember(dest => dest.PathToThumbnail, opt => opt.MapFrom<PathValueResolver, string>(src =>
                        src.ThumbnailFileName));
            });
        }
    }
}
