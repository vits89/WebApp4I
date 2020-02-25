using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApp4I.AppCore.Entities;
using WebApp4I.AppCore.Interfaces;

namespace WebApp4I.Infrastructure.Data
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

        public Task<IEnumerable<ImageInfo>> GetAllAsync()
        {
            return Task.FromResult(_dbContext.ImageInfo.AsEnumerable());
        }

        public async Task UpdateAsync(ImageInfo imageInfo)
        {
            _dbContext.Entry(imageInfo).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
