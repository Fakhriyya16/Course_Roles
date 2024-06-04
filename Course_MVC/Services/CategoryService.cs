using Course_MVC.Data;
using Course_MVC.Models;
using Course_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Course_MVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> ExistCategory(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<SelectList> GetAllCategoriesSelectList()
        {
            var categories = await _context.Categories.ToListAsync();
            return new SelectList(categories, "Id", "Name");
        }

        public async Task<List<Category>> GetAllCategoriesWithImages()
        {
            return await _context.Categories.Include(m=>m.Images).ToListAsync();
        }

        public async Task<Category> GetByIdWithAllData(int id)
        {
            return await _context.Categories.Include(m => m.Images).Include(m=>m.Courses).ThenInclude(m=>m.Images).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CategoryImage> GetImageByIdAsync(int id)
        {
            return await _context.CategoryImages.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
