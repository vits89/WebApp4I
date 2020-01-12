using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp4I.Models
{
    public class ImageInfoRepository : IImageInfoRepository
    {
        private readonly AppDbContext _dbContext = new AppDbContext();

        public async Task AddAsync(ImageInfo imageInfo)
        {
            _dbContext.ImageInfo.Add(imageInfo);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<ImageInfo> imageInfo)
        {
            _dbContext.ImageInfo.AddRange(imageInfo);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ImageInfo> GetAsync(int id)
        {
            return await _dbContext.ImageInfo.FindAsync(id);
        }

        public IEnumerable<ImageInfo> GetAll()
        {
            return _dbContext.ImageInfo.AsEnumerable();
        }

        public async Task<bool> UpdateAsync(ImageInfo imageInfo)
        {
            var existingImageInfo = await _dbContext.ImageInfo.FindAsync(imageInfo.Id);

            if (existingImageInfo != null)
            {
                existingImageInfo.Description = imageInfo.Description;

                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
