using System.Collections.Generic;

namespace WebApp4I.WebApp.Models
{
    public class PostedFileModel
    {
        public string Name { get; set; }
        public IEnumerable<byte> Content { get; set; }
    }
}
