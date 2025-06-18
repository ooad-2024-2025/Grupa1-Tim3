using Nutritionist.Models;
using System.ComponentModel.DataAnnotations;

namespace Nutritionist.ViewModels
{
    public class RecipeCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public RecipeType Type { get; set; }

        [Required]
        public string TimeToMake { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Display(Name = "Slika")]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
