using System;
using System.ComponentModel.DataAnnotations;

namespace RecipePlatform.Models
{
    public class Rating : BaseModel
    {
       

        [Range(1, 5)]
        public int Score { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        public DateTime RatedDate { get; set; } = DateTime.Now;

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
