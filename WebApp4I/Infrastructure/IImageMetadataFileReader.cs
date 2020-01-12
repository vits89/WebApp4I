using System.Collections.Generic;

namespace WebApp4I.Infrastructure
{
    public interface IImageMetadataFileReader
    {
        IDictionary<string, string> Read(string fileName);
    }
}
