using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Commerce.AuthAPI.Models.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Username { get; set; }

        [Display(Name = "youremail@gmail.com")]
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
    }
}
