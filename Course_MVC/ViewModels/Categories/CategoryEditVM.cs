using Course_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Course_MVC.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public List<IFormFile> NewImages { get; set; }
        public List<CategoryImage> AvailableImages { get; set; }
    }
}
