namespace Course_MVC.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool SoftDelete { get; set; }
    }
}
