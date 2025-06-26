
using Microsoft.AspNetCore.Identity;

namespace RecipePlatform.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string Gender { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
