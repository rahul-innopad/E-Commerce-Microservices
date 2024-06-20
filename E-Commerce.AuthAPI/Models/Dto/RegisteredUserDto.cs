using System.ComponentModel.DataAnnotations;

namespace E_Commerce.AuthAPI.Models.Dto
{
    public class RegisteredUserDto
    {
        public string Name { get; set; }
        public string Username { get; set; }

        [Display(Name = "youremail@gmail.com")]
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfrimPassword { get; set; }

        [Display(Name = "9900114477")]
        public string Phone { get; set; }
    }
}
