using Course_MVC.Data;
using Course_MVC.Models;
using Course_MVC.Services.Interfaces;
using Course_MVC.ViewModels.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Course_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;
        public CategoryController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, AppDbContext appDbContext)
        {
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;

        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesWithImages();

            List<CategoryTableVM> model = categories.Select(m => new CategoryTableVM
            {
                Id = m.Id,
                Name = m.Name,
                Image = m.Images.FirstOrDefault(m=>m.isMain).Name,
                Description = m.Description,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy")
            }).ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(category.Images is null)
            {
                ModelState.AddModelError("Image", "You have to include image");
                return View();
            }

            foreach (var item in category.Images)
            {
                if (!(item.Length / 1024 < 500))
                {
                    ModelState.AddModelError("Image", "Image max size is 200KB");
                    return View();
                }
            }
            if (await _categoryService.ExistCategory(category.Name))
            {
                ModelState.AddModelError("Name", "Category with this title already exists");
                return View();
            }

            List<CategoryImage> images = new();

            foreach (var item in category.Images)
            {
                string fileName = Guid.NewGuid().ToString() + item.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                images.Add(new CategoryImage { Name = fileName });
            }

            images.FirstOrDefault().isMain = true;

            Category newCategory = new()
            {
                Name = category.Name,
                Description = category.Description,
                Images = images,
                CreatedDate = DateTime.Now,
            };

            await _categoryService.Create(newCategory);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetByIdWithAllData((int)id);

            if (existCategory is null) return NotFound();

            foreach (var item in existCategory.Images)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", item.Name);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            await _categoryService.DeleteAsync(existCategory);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetByIdWithAllData((int)id);

            if (existCategory is null) return NotFound();

            CategoryDetailVM model = new()
            {
                Name = existCategory.Name,
                Description = existCategory.Description,
                Courses = existCategory.Courses.ToList(),
                Image = existCategory.Images.FirstOrDefault(m=>m.isMain).Name,
                CreatedDate = existCategory.CreatedDate.ToString("dd.MM.yyyy")
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetByIdWithAllData((int)id);

            if (existCategory is null) return NotFound();

            CategoryEditVM model = new()
            {
                Name = existCategory.Name,
                Description = existCategory.Description,
                AvailableImages = existCategory.Images.ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM model)
        {
            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetByIdWithAllData((int)id);

            if (existCategory is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if(model.NewImages is not null)
            {
                foreach (var item in model.NewImages)
                {
                    if (!(item.Length / 1024 < 500))
                    {
                        ModelState.AddModelError("Image", "Image max size is 200KB");
                        return View();
                    }
                }

                List<CategoryImage> images = new();

                foreach (var item in model.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + item.FileName;

                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                    using (FileStream stream = new(path, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    images.Add(new CategoryImage { Name = fileName });
                }
                existCategory.Images = images;
            }



            existCategory.Name = model.Name;
            existCategory.Description = model.Description;


            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id is null) return BadRequest();

            var image = await _categoryService.GetImageByIdAsync((int)id);

            if (image is null) return NotFound();

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", image.Name);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            _appDbContext.CategoryImages.Remove(image);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> MakeMain(int? id)
        {
            if (id is null) return BadRequest();

            var image = await _categoryService.GetImageByIdAsync((int)id);

            var category = await _categoryService.GetByIdWithAllData(image.CategoryId);

            if (image is null) return NotFound();

            if (category.Images.Count == 0) return NotFound();

            foreach (var item in category.Images)
            {
                item.isMain = false;
                _appDbContext.CategoryImages.Update(item);
            }

            image.isMain = true;
            _appDbContext.CategoryImages.Update(image);

            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
