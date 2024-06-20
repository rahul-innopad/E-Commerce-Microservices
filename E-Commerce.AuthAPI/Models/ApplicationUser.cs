using Microsoft.AspNetCore.Identity;

namespace E_Commerce.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
