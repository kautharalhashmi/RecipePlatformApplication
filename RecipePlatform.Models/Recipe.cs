using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipePlatform.Models
{
    public class Recipe: BaseModel
    {
      

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Range(0, int.MaxValue)]
        public int PrepTimeMinutes { get; set; }

        [Range(0, int.MaxValue)]
        public int CookTimeMinutes { get; set; }

        [Range(1, 100)]
        public int Servings { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}
