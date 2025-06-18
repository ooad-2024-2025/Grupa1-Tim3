using Nutritionist.Models;
using System.ComponentModel.DataAnnotations;

namespace Nutritionist.ViewModels
{
    public class RecipeEditViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public RecipeType Type { get; set; }

        [Required]
        public string TimeToMake { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Ingredients { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
