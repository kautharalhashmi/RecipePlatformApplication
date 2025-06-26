using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models;

namespace RecipePlatform.BLL.Interface
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<IEnumerable<Recipe>> GetUserRecipesAsync(string userId);
        Task<Recipe?> GetByIdAsync(int id);
        Task AddAsync(Recipe recipe);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(int id);
        Task<IEnumerable<Recipe>> SearchRecipesAsync(string term);
        Task<bool> RateRecipeAsync(int recipeId, string userId, int score);


    }
}
