using System.Collections.Generic;
using System.IO;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using WebApp4I.AppCore.Interfaces;

namespace WebApp4I.Infrastructure.Services
{
    public class ImageMetadataFileReader : IImageMetadataFileReader
    {
        private readonly string _path;

        public ImageMetadataFileReader(string path)
        {
            _path = path;
        }

        public IDictionary<string, string> Read(string fileName)
        {
            Dictionary<string, string> metadata;

            using (var stream = GetFileStream(fileName, FileMode.Open))
            {
                var tags = ImageMetadataReader.ReadMetadata(stream)
                    .Where(d => d is ExifIfd0Directory || d is ExifSubIfdDirectory || d is ExifImageDirectory ||
                        d is GpsDirectory)
                    .SelectMany(d => d.Tags)
                    .Where(t => !string.IsNullOrWhiteSpace(t.Description));

                metadata = tags.ToDictionary(t => t.Name, t => t.Description);
            }

            return metadata;
        }

        protected virtual Stream GetFileStream(string fileName, FileMode mode)
        {
            return new FileStream(Path.Combine(_path, fileName), mode);
        }
    }
}
