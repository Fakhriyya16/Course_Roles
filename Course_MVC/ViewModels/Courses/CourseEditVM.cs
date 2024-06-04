using Course_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Course_MVC.ViewModels.Courses
{
    public class CourseEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<CourseImage> AvailableImages { get; set; }
        public List<IFormFile> NewImages { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string DiscountedPrice { get; set; }
    }
}
