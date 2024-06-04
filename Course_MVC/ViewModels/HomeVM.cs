using Course_MVC.ViewModels.Categories;
using Course_MVC.ViewModels.Courses;

namespace Course_MVC.ViewModels
{
    public class HomeVM
    {
        public List<CategoryHomeVM> Categories { get; set; }
        public List<CourseHomeVM> Courses { get; set; }
    }
}
