using Microsoft.AspNetCore.Identity;

namespace E_Commerce.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLogin { get; set; }
        public string UniqueIdentifire { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
