namespace Nutritionist.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
