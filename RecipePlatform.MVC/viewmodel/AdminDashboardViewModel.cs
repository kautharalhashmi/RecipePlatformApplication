using RecipePlatform.Models;

namespace RecipePlatform.MVC.viewmodel
{
 
        public class AdminDashboardViewModel
        {
        public List<ApplicationUser> Users { get; set; }  // ✅ List, not IQueryable
        public IEnumerable<Recipe> Recipes { get; set; }
    }
    

}
