using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.Models;

namespace RecipePlatform.DAL.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task DeleteAsync(string userId);
    }
}
