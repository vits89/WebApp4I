using System.Collections.Generic;

namespace WebApp4I.WebApp.ViewModels
{
    public class ImageInfoViewModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
