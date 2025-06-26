using RecipePlatform.BLL.Interface;
using RecipePlatform.DAL.Interface;
using RecipePlatform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipePlatform.BLL.Repo_s
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task AddAsync(Category category)
        {
            await _categoryRepo.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepo.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Category category)
        {
            await _categoryRepo.UpdateAsync(category);
        }
    }
}
