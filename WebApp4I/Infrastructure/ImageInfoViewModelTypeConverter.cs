using System.Collections.Generic;
using AutoMapper;
using WebApp4I.Models;
using WebApp4I.ViewModels;

namespace WebApp4I.Infrastructure
{
    public class ImageInfoViewModelTypeConverter : ITypeConverter<ImageInfo, ImageInfoViewModel>
    {
        public ImageInfoViewModel Convert(ImageInfo source, ImageInfoViewModel destination, ResolutionContext context)
        {
            if (!context.Items.TryGetValue("path", out var path))
            {
                path = "/Content";
            }

            var pathToImages = (string)path;

            context.Items.TryGetValue("metadata", out var metadata);

            return new ImageInfoViewModel
            {
                Id = source.Id,
                Path = $"{pathToImages}/{source.FileName}",
                PathToThumbnail = $"{pathToImages}/{source.ThumbnailFileName}",
                Description = source.Description,
                Metadata = metadata as IDictionary<string, string>
            };
        }
    }
}
