using Nutritionist.Models;

namespace Nutritionist.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public bool IsAdmin { get; set; }

        public List<Recipe> FavoriteRecipes { get; set; } = new();
    }
}
