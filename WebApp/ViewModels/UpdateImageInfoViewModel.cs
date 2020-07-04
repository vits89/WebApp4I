using System.ComponentModel.DataAnnotations;

namespace WebApp4I.WebApp.ViewModels
{
    public class UpdateImageInfoViewModel
    {
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
