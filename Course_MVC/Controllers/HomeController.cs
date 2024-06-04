using Course_MVC.Services.Interfaces;
using Course_MVC.ViewModels;
using Course_MVC.ViewModels.Categories;
using Course_MVC.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Course_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICourseService _courseService;
        public HomeController(ICategoryService categoryService, ICourseService courseService)
        {
            _categoryService = categoryService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesWithImages();
            var courses = await _courseService.GetAllCoursesWithFullData();

            HomeVM model = new()
            {
                Categories = categories.Select(m => new CategoryHomeVM()
                {
                    Name = m.Name,
                    Description = m.Description,
                    Image = m.Images.FirstOrDefault()?.Name
                }).ToList(),
                Courses = courses.Select(m=> new CourseHomeVM
                {
                    CategoryName = m.Category.Name,
                    Image = m.Images.FirstOrDefault(m=>m.isMain).Name,
                    Name = m.Name,
                    Teacher = m.Teacher,
                    DiscountedPrice = m.DiscountedPrice,
                    Price = m.Price,
                }).ToList(),
            };
            return View(model);
        }
    }
}
