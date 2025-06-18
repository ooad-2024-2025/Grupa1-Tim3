using System.ComponentModel.DataAnnotations;

namespace Nutritionist.ViewModels
{
    public class ReviewCreateViewModel
    {
        public Guid RecipeId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Comment { get; set; }
    }
}
