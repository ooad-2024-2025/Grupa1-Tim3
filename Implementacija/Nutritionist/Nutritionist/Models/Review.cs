using System.ComponentModel.DataAnnotations;

namespace Nutritionist.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string? Comment { get; set; }
        [Range(1,5)]
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
