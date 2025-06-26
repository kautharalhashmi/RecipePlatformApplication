using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipePlatform.Models
{
    public class Category : BaseModel
    {
     

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
