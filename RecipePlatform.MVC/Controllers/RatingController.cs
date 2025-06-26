using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.Interface;
using RecipePlatform.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipePlatform.MVC.Controllers
{
    [Authorize(Roles = "User")]
    public class RatingController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IRatingService _ratingService;

        public RatingController(IRecipeService recipeService, IRatingService ratingService)
        {
            _recipeService = recipeService;
            _ratingService = ratingService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitRating(int recipeId, int score, string comment)
        {
            if (score < 1 || score > 5)
                return BadRequest("Score must be between 1 and 5");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Prevent duplicate rating by the same user
            var alreadyRated = (await _ratingService.GetByRecipeIdAsync(recipeId))
                .Any(r => r.UserId == userId);

            if (alreadyRated)
                return BadRequest("You have already rated this recipe.");

            // Add new rating
            var rating = new Rating
            {
                RecipeId = recipeId,
                UserId = userId,
                Score = score,
                Comment = comment,
                RatedDate = DateTime.UtcNow
            };

            await _ratingService.AddAsync(rating);

            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }
    }
}
