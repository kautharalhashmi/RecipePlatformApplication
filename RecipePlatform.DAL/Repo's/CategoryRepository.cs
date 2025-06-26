using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.BLL.Interface;
using RecipePlatform.DAL.Interface;
using RecipePlatform.DAL.Repo_s;
using RecipePlatform.Models;

namespace RecipePlatform.DAL.Repo_s
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IGenericRepository<Category> _categoryRepo;

        public CategoryRepository(IGenericRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepo.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepo.GetByIdAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _categoryRepo.UpdateAsync(category);
            await _categoryRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category != null)
                _categoryRepo.DeleteAsync(category.Id);
            await _categoryRepo.SaveAsync();
        }

      

   

       

    }
}
