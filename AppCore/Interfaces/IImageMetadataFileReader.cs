using System.Collections.Generic;

namespace WebApp4I.AppCore.Interfaces
{
    public interface IImageMetadataFileReader
    {
        IDictionary<string, string> Read(string fileName);
    }
}
