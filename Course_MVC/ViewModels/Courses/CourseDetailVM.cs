namespace Course_MVC.ViewModels.Courses
{
    public class CourseDetailVM
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string CreatedDate { get; set; }
        public string Teacher { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string MainImage { get; set; }
        public List<string> Images { get; set; }
    }
}
