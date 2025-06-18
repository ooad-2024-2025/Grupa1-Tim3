using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Nutritionist.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public RecipeType Type { get; set; }
        public string TimeToMake { get; set; }
        public string Ingredients { get; set; }
        public Guid UserId { get; set; }
        public string? ImageUrl { get; set; }
        [ValidateNever] public User User { get; set; }

        [ValidateNever] public ICollection<Favorite> Favorites { get; set; }
        [ValidateNever] public ICollection<Review> Reviews { get; set; }
    }

    public enum RecipeType
    {
        Dorucak,
        Rucak,
        Vecera,
        Desert,
        Salata,
        ZaPiti
    }
}
