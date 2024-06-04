using Course_MVC.Models;

namespace Course_MVC.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesWithFullData();
        Task Create(Course course);
        Task<bool> ExistCourse(string name);
        Task DeleteAsync(Course course);
        Task<Course> GetByIdWithAllDataAsync(int id);
        Task<CourseImage> GetImageByIdAsync(int id);
    }
}
