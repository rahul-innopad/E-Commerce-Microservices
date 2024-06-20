using E_Commerce.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.AuthAPI.Repository
{
    public class PasswordHasherRepository : IPasswordHasher<ApplicationUser>
    {
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        public PasswordHasherRepository(PasswordHasher<ApplicationUser> passwordHasher)
        {
            this._passwordHasher=passwordHasher;
        }
        public string HashPassword(ApplicationUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }
}
