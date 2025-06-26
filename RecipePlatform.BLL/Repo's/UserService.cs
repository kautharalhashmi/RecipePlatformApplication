using RecipePlatform.BLL.Interface;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipePlatform.BLL.Repo_s
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Recipes)
                .Include(u => u.Ratings)
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Recipes)
                .Include(u => u.Ratings)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task DeleteAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
