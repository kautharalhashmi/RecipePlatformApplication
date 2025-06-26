using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models;

namespace RecipePlatform.DAL.Interface
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetByRecipeIdAsync(int recipeId);
        Task AddAsync(Rating rating);
        Task DeleteAsync(int id);

    }
}
