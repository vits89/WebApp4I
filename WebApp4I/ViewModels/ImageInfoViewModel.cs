using System.Collections.Generic;

namespace WebApp4I.ViewModels
{
    public class ImageInfoViewModel
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
