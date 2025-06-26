using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipePlatform.BLL.Interface;
using RecipePlatform.BLL.Repo_s;
using RecipePlatform.Models;
using RecipePlatform.MVC.viewmodel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipePlatform.MVC.Controllers
{
    [Authorize(Roles = "User")]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        private readonly IRatingService _ratingService;

        public RecipeController(IRecipeService recipeService, ICategoryService categoryService, IRatingService ratingService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
            _ratingService = ratingService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipes = await _recipeService.GetUserRecipesAsync(userId);
            return View(recipes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Recipe model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }

            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _recipeService.AddAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null) return NotFound();

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Recipe model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }

            await _recipeService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _recipeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var recipes = await _recipeService.GetAllRecipesAsync(); // All recipes from all users
            return View(recipes);
        }


        //public async Task<IActionResult> All()
        //{
        //    var categories = await _categoryService.GetAllAsync(); // returns List<Category>

        //    var categorySelectList = categories
        //        .Select(c => new SelectListItem
        //        {
        //            Value = c.Id.ToString(),
        //            Text = c.Name
        //        }).ToList();

        //    var viewModel = new RecipeListViewModel
        //    {
        //        Categories = categorySelectList,
        //        Recipes = await _recipeService.GetAllRecipesAsync()
        //    };

        //    return View(viewModel);
        //}


    //[AllowAnonymous]
    //public async Task<IActionResult> Details(int id)
    //{
    //    var recipe = await _recipeService.GetByIdAsync(id);
    //    if (recipe == null)
    //        return NotFound();

    //    return View(recipe);
    //}

    [AllowAnonymous]
        public async Task<IActionResult> AllHome()
        {
            var recipes = await _recipeService.GetAllRecipesAsync(); // All recipes from all users
            return View(recipes);
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var recipe = await _recipeService.GetByIdAsync(id);
        //    if (recipe == null)
        //        return NotFound();

        //    return View(recipe);
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null) return NotFound();

            var ratings = await _ratingService.GetByRecipeIdAsync(id);
            ViewBag.Ratings = ratings;

            return View(recipe);
        }

    }
}
