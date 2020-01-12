using AutoMapper;
using WebApp4I.Infrastructure;
using WebApp4I.Models;
using WebApp4I.ViewModels;

namespace WebApp4I
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configuration { get; private set; }

        public static void Initialize()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImageInfo, ImageInfoViewModel>().ConvertUsing<ImageInfoViewModelTypeConverter>();
            });
        }
    }
}
