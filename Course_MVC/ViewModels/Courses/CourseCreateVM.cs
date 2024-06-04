using System.ComponentModel.DataAnnotations;

namespace Course_MVC.ViewModels.Courses
{
    public class CourseCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string DiscountedPrice { get; set; }
    }
}
