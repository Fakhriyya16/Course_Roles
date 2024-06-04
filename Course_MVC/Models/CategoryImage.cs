using System.ComponentModel.DataAnnotations.Schema;

namespace Course_MVC.Models
{
    public class CategoryImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isMain { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
    }
}
