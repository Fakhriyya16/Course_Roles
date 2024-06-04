using System.ComponentModel.DataAnnotations.Schema;

namespace Course_MVC.Models
{
    public class Course: BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<CourseImage> Images { get; set; }
        public string Teacher { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
