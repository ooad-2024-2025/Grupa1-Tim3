using Nutritionist.Models;
using System.ComponentModel.DataAnnotations;

namespace Nutritionist.ViewModels
{
    public class RecipeCreateViewModel
    {
        [Display(Name = "Naslov")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Tip Recepta")]
        [Required]
        public RecipeType Type { get; set; }

        [Display(Name = "Vrijeme pripreme")]
        [Required]
        public string TimeToMake { get; set; }

        [Display(Name = "Sastojci")]
        [Required]
        public string Ingredients { get; set; }

        [Display(Name = "Slika")]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
