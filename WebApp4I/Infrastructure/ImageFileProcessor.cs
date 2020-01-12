﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace WebApp4I.Infrastructure
{
    public class ImageFileProcessor : IImageFileProcessor
    {
        private const int THUMBNAIL_SIZE = 400;

        private readonly string _path;

        public ImageFileProcessor(string path)
        {
            _path = path;
        }

        public async Task<string> SaveAsync(byte[] content, string fileName)
        {
            var newFileName = GetUniqueFileName(fileName);

            using (var stream = GetFileStream(newFileName, FileMode.Create))
            {
                await stream.WriteAsync(content, offset: 0, count: content.Length);
            }

            return newFileName;
        }

        public async Task<string> CreateThumbnailAsync(byte[] content, string fileName)
        {
            var frameInfo = ImageFileInfo.Load(content).Frames.First();

            var settings = new ProcessImageSettings
            {
                Width = frameInfo.Width >= frameInfo.Height ? THUMBNAIL_SIZE : 0,
                Height = frameInfo.Height > frameInfo.Width ? THUMBNAIL_SIZE : 0,
                HybridMode = HybridScaleMode.Off,
                ResizeMode = CropScaleMode.Contain
            };

            var newFileName = GetUniqueFileName(GetThumbnailFileName(fileName));

            using (var stream = GetFileStream(newFileName, FileMode.Create))
            {
                MagicImageProcessor.ProcessImage(content, stream, settings);
            }

            return await Task.FromResult(newFileName);
        }

        protected virtual Stream GetFileStream(string fileName, FileMode mode)
        {
            return new FileStream(Path.Combine(_path, fileName), mode);
        }

        protected virtual string GetThumbnailFileName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName) + "_thumbnail" + Path.GetExtension(fileName);
        }

        protected virtual string GetUniqueFileName(string fileName)
        {
            if (!File.Exists(Path.Combine(_path, fileName)))
            {
                return fileName.Substring(0);
            }

            var newFileName = Path.GetFileNameWithoutExtension(fileName);

            newFileName += "_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

            return newFileName + Path.GetExtension(fileName);
        }
    }
}
