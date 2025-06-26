using System.ComponentModel.DataAnnotations;
using RecipePlatform.Models;  // For DifficultyLevel enum

namespace RecipePlatform.Models.ViewModel
{
    public class RecipeFormViewModel
    {
        public int? Id { get; set; } // Nullable for create/edit distinction

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ingredients are required.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Instructions are required.")]
        public string Instructions { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Preparation time must be zero or positive.")]
        public int? PrepTimeMinutes { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Cook time must be zero or positive.")]
        public int? CookTimeMinutes { get; set; }

        [Range(1, 100, ErrorMessage = "Servings must be between 1 and 100.")]
        public int? Servings { get; set; }

        [Required(ErrorMessage = "Difficulty is required.")]
        public DifficultyLevel? Difficulty { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int? CategoryId { get; set; }
    }
}
