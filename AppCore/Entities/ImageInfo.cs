using System.ComponentModel.DataAnnotations;

namespace WebApp4I.AppCore.Entities
{
    public class ImageInfo
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string FileName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(90)]
        public string ThumbnailFileName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
