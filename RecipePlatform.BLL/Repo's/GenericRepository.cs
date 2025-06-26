//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using RecipePlatform.BLL.Interface;
//using RecipePlatform.DAL.Context;
//using RecipePlatform.Models;

//namespace RecipePlatform.BLL.Repo_s
//{
//    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly DbSet<T> _dbSet;

//        public GenericRepository(ApplicationDbContext context)
//        {
//            _context = context;
//            _dbSet = _context.Set<T>();
//        }

//        public async Task<IEnumerable<T>> GetAllAsync()
//        {
//            return await _dbSet.ToListAsync();
//        }

//        public async Task<T?> GetByIdAsync(int id)
//        {
//            return await _dbSet.FindAsync(id);
//        }

//        public async Task AddAsync(T entity)
//        {
//            await _dbSet.AddAsync(entity);
//            await _context.SaveChangesAsync();
//        }

//        public async Task Update(T entity)
//        {
//            _dbSet.Update(entity);
//            await _context.SaveChangesAsync();
//        }

//        public async Task Delete(int id)
//        {
//            var entity = await GetByIdAsync(id);
//            if (entity != null)
//            {
//                _dbSet.Remove(entity);
//                await _context.SaveChangesAsync();
//            }
//        }

//        public Task SaveAsync()
//        {
            
//            return _context.SaveChangesAsync();
//        }
//    }
//}
