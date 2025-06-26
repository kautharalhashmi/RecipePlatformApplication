using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models;

namespace RecipePlatform.BLL.Interface
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetByRecipeIdAsync(int recipeId);
        Task AddAsync(Rating rating);
        Task DeleteAsync(int id);
        Task<bool> RateRecipeAsync(int recipeId, string userId, int score);


    }
}
