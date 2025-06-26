using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.Interface;
using RecipePlatform.Models;
using System.Security.Claims;

namespace RecipePlatform.MVC.Controllers
{
    [Authorize(Roles = "User")]
    public class UserDashboardController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;

        public UserDashboardController(
            IRecipeService recipeService,
            ICategoryService categoryService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipes = await _recipeService.GetUserRecipesAsync(userId);
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            return View(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Recipe recipe)
        {
            recipe.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (recipe.Id == 0)
                await _recipeService.AddAsync(recipe);
            else
                await _recipeService.UpdateAsync(recipe);

            return RedirectToAction(nameof(Index));
        }

      
        [HttpGet]
        public async Task<IActionResult> EditForm(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();

            var result = new
            {
                id = recipe.Id,
                title = recipe.Title,
                description = recipe.Description,
                ingredients = recipe.Ingredients,
                instructions = recipe.Instructions,
                prepTimeMinutes = recipe.PrepTimeMinutes,
                cookTimeMinutes = recipe.CookTimeMinutes,
                servings = recipe.Servings,
                difficulty = recipe.Difficulty,
                categoryId = recipe.CategoryId
            };

            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _recipeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
