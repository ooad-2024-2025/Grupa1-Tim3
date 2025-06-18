using Nutritionist.Models;

namespace Nutritionist.ViewModels
{
    public class RecipeListViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<RecipeType> RecipeTypes { get; set; }
        public RecipeType? SelectedType { get; set; }
    }
}
