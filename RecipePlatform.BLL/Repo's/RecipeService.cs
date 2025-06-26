using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using RecipePlatform.DAL.Interface;
using RecipePlatform.BLL.Interface;

namespace RecipePlatform.BLL.Repo_s
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipesAsync(string userId)
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string term)
        {
            return await _context.Recipes
                .Where(r => r.Title.Contains(term) || r.Ingredients.Contains(term))
                .ToListAsync();
        }

        public async Task<bool> RateRecipeAsync(int recipeId, string userId, int value)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null) return false;

            var existing = await _context.Ratings
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);

            if (existing != null)
            {
                existing.Score = value;
                existing.RatedDate = DateTime.Now;
            }
            else
            {
                _context.Ratings.Add(new Rating
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    Score = value,
                    RatedDate = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.User)
                .ToListAsync();
        }

    
    }
}
