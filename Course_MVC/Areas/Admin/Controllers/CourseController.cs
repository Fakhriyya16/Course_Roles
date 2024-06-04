using Course_MVC.Data;
using Course_MVC.Models;
using Course_MVC.Services.Interfaces;
using Course_MVC.ViewModels.Categories;
using Course_MVC.ViewModels.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        public CourseController(ICourseService courseService, IWebHostEnvironment webHostEnvironment, ICategoryService categoryService, AppDbContext context)
        {
            _courseService = courseService;
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesWithFullData();

            List<CourseTableVM> model = courses.Select(m=> new CourseTableVM
            {
                Id = m.Id,
                Name = m.Name,
                CategoryName = m.Category.Name,
                Image = m.Images.FirstOrDefault(m=>m.isMain)?.Name,
                Teacher = m.Teacher,
                Price = m.Price.ToString(m.Price % 1 == 0 ? "0" : "0.00"),
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy"),
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM course)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesSelectList();
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (course.Images is null)
            {
                ModelState.AddModelError("Image", "You have to include image");
                return View();
            }

            foreach (var item in course.Images)
            {
                if (!(item.Length / 1024 < 500))
                {
                    ModelState.AddModelError("Image", "Image max size is 200KB");
                    return View();
                }
            }
            if (await _courseService.ExistCourse(course.Name))
            {
                ModelState.AddModelError("Name", "Course with this name already exists");
                return View();
            }

            List<CourseImage> images = new();

            foreach (var item in course.Images)
            {
                string fileName = Guid.NewGuid().ToString() + item.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                images.Add(new CourseImage { Name = fileName });
            }

            images.FirstOrDefault().isMain = true;

            Course newCourse = new()
            {
                Name = course.Name,
                Teacher = course.TeacherName,
                Images = images,
                CreatedDate = DateTime.Now,
                Price = decimal.Parse(course.Price.Replace(".", ",")),
                DiscountedPrice = decimal.Parse(course.DiscountedPrice.Replace(".", ",")),
                CategoryId = course.CategoryId
            };

            await _courseService.Create(newCourse);
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existCourse = await _courseService.GetByIdWithAllDataAsync((int)id);

            if (existCourse is null) return NotFound();

            foreach (var item in existCourse.Images)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", item.Name);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            await _courseService.DeleteAsync(existCourse);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDataAsync((int)id);

            if (course is null) return NotFound();

            CourseDetailVM model = new()
            {
                Name = course.Name,
                CategoryName = course.Category.Name,
                Images = course.Images.Where(m => !m.isMain).Select(m => m.Name).ToList(),
                CreatedDate = course.CreatedDate.ToString("dd.MM.yyyy"),
                Teacher = course.Teacher,
                Price = course.Price,
                MainImage = course.Images.FirstOrDefault(m => m.isMain).Name,
                DiscountedPrice = course.DiscountedPrice
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesSelectList();
            if (id is null) return BadRequest();

            var existCourse = await _courseService.GetByIdWithAllDataAsync((int)id);

            if (existCourse is null) return NotFound();

            CourseEditVM model = new()
            {
                Name = existCourse.Name,
                CategoryId = existCourse.CategoryId,
                AvailableImages = existCourse.Images.ToList(),
                TeacherName = existCourse.Teacher,
                Price = existCourse.Price.ToString().Replace(",", "."),
                DiscountedPrice = existCourse.DiscountedPrice.ToString().Replace(",", ".")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM model)
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesSelectList();
            if (id is null) return BadRequest();

            var existCourse = await _courseService.GetByIdWithAllDataAsync((int)id);

            if (existCourse is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.NewImages is not null)
            {
                foreach (var item in model.NewImages)
                {
                    if (!(item.Length / 1024 < 500))
                    {
                        ModelState.AddModelError("Image", "Image max size is 500KB");
                        return View();
                    }
                }

                List<CourseImage> images = new();

                foreach (var item in model.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + item.FileName;

                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                    using (FileStream stream = new(path, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    images.Add(new CourseImage { Name = fileName });
                }
                existCourse.Images = images;
            }



            existCourse.Name = model.Name;
            existCourse.CategoryId = model.CategoryId;
            existCourse.Teacher = model.TeacherName;
            existCourse.Price = decimal.Parse(model.Price.Replace(".", ","));
            existCourse.DiscountedPrice = decimal.Parse(model.DiscountedPrice.Replace(".", ","));


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id is null) return BadRequest();

            var image = await _courseService.GetImageByIdAsync((int)id);

            if (image is null) return NotFound();

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", image.Name);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            _context.CourseImages.Remove(image);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> MakeMain(int? id)
        {
            if (id is null) return BadRequest();

            var image = await _courseService.GetImageByIdAsync((int)id);

            var course = await _courseService.GetByIdWithAllDataAsync(image.CourseId);

            if (image is null) return NotFound();

            if (course.Images.Count == 0) return NotFound();

            foreach (var item in course.Images)
            {
                item.isMain = false;
                _context.CourseImages.Update(item);
            }

            image.isMain = true;
            _context.CourseImages.Update(image);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
