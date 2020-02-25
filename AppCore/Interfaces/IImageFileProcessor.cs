using System.Threading.Tasks;

namespace WebApp4I.AppCore.Interfaces
{
    public interface IImageFileProcessor
    {
        Task<string> SaveAsync(byte[] content, string fileName);
        Task<string> CreateThumbnailAsync(byte[] content, string fileName);
    }
}
