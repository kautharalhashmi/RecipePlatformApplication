using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecipePlatform.Models; // Assuming Recipe class is here

namespace RecipePlatform.Models.ViewModel
{
    public class RecipeViewModel
    {
        public int? Id { get; set; } // null for new recipe, set for edit

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public int? PrepTimeMinutes { get; set; }

        public int? CookTimeMinutes { get; set; }

        public int? Servings { get; set; }

        public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Easy;

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public bool IsEditing { get; set; } = false;

        public RecipeFormViewModel NewRecipe { get; set; } = new RecipeFormViewModel();


    }
}
