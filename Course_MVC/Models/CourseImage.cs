using System.ComponentModel.DataAnnotations.Schema;

namespace Course_MVC.Models
{
    public class CourseImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isMain { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
    }
}
