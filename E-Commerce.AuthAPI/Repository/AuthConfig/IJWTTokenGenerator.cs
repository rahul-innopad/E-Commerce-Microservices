using E_Commerce.AuthAPI.Models;

namespace E_Commerce.AuthAPI.Repository.AuthConfig
{
    public interface IJWTTokenGenerator
    {
       string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles, string role);
    }
}
