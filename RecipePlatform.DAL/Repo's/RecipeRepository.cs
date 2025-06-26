using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipePlatform.DAL.Context;
using RecipePlatform.DAL.Interface;
using RecipePlatform.Models;

namespace RecipePlatform.DAL.Repo_s
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipesAsync(string userId)
        {
            return await _context.Recipes
                .Include(r => r.Category)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

       
        public Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddRecipeAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecipeAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecipeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> SearchRecipesAsync(string term)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RateRecipeAsync(int recipeId, string userId, int value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
