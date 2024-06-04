using Course_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course_MVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesWithImages();
        Task<bool> ExistCategory(string name);
        Task Create(Category category);
        Task<Category> GetByIdWithAllData(int id);
        Task DeleteAsync(Category category);
        Task<SelectList> GetAllCategoriesSelectList();
        Task<CategoryImage> GetImageByIdAsync(int id);
    }
}
