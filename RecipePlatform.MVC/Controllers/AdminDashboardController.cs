using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.Interface;
using RecipePlatform.Models;
using RecipePlatform.MVC.viewmodel;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlatform.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminDashboardController(IRecipeService recipeService, UserManager<ApplicationUser> userManager)
        {
            _recipeService = recipeService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList(); 


            var recipes = await _recipeService.GetAllRecipesAsync();

            var model = new AdminDashboardViewModel
            {
                Users = users,
                Recipes = recipes
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            await _recipeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
