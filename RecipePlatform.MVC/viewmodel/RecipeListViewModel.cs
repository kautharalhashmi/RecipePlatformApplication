using RecipePlatform.Models;

namespace RecipePlatform.MVC.viewmodel
{
    public class RecipeListViewModel
    {
        public List<Recipe> Recipes { get; set; }
        public List<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SearchTerm { get; set; }
    }
}
