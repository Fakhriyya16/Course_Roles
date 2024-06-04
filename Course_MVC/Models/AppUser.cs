using Microsoft.AspNetCore.Identity;

namespace Course_MVC.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
