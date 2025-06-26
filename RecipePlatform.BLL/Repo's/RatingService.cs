using RecipePlatform.BLL.Interface;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlatform.BLL.Repo_s
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetByRecipeIdAsync(int recipeId)
        {
            return await _context.Ratings
                .Include(r => r.User)
                .Where(r => r.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task AddAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RateRecipeAsync(int recipeId, string userId, int score)
        {
            if (score < 1 || score > 5)
                return false;

            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null)
                return false;

            var existingRating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);

            if (existingRating != null)
            {
                // Update existing rating
                existingRating.Score = score;
                existingRating.RatedDate = DateTime.UtcNow;
            }
            else
            {
                // Create new rating
                var rating = new Rating
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    Score = score,
                    RatedDate = DateTime.UtcNow
                };
                _context.Ratings.Add(rating);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
