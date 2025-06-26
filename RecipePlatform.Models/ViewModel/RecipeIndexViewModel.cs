using System.Collections.Generic;

namespace RecipePlatform.Models.ViewModel
{
    public class RecipeIndexViewModel
    {
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public RecipeFormViewModel NewRecipe { get; set; } = new RecipeFormViewModel();

        public bool IsEditing { get; set; } = false;

        // Add categories dropdown for form (optional but recommended)
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
