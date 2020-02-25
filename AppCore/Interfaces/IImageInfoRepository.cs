using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp4I.AppCore.Entities;

namespace WebApp4I.AppCore.Interfaces
{
    public interface IImageInfoRepository
    {
        Task AddAsync(ImageInfo imageInfo);
        Task AddAsync(IEnumerable<ImageInfo> imageInfo);
        Task<ImageInfo> GetAsync(int id);
        Task<IEnumerable<ImageInfo>> GetAllAsync();
        Task UpdateAsync(ImageInfo imageInfo);
    }
}
