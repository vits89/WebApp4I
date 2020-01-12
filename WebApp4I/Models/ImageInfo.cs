using System.ComponentModel.DataAnnotations;

namespace WebApp4I.Models
{
    public class ImageInfo
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public string ThumbnailFileName { get; set; }
        public string Description { get; set; }
    }
}
