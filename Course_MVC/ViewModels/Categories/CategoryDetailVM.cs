using Course_MVC.Models;

namespace Course_MVC.ViewModels.Categories
{
    public class CategoryDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Course> Courses { get; set; }
        public string Image { get; set; }
        public string CreatedDate { get; set; }
    }
}
