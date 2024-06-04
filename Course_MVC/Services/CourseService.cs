using Course_MVC.Data;
using Course_MVC.Models;
using Course_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Course_MVC.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();  
        }

        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistCourse(string name)
        {
            return await _context.Courses.AnyAsync(c => c.Name == name);
        }

        public async Task<List<Course>> GetAllCoursesWithFullData()
        {
            return await _context.Courses.Include(m=>m.Category).Include(m=>m.Images).ToListAsync();
        }

        public async Task<Course> GetByIdWithAllDataAsync(int id)
        {
            return await _context.Courses.Include(m => m.Category).Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CourseImage> GetImageByIdAsync(int id)
        {
            return await _context.CourseImages.FirstOrDefaultAsync(m=>m.Id ==  id);
        }
    }
}
