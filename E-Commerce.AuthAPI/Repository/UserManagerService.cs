using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.AuthAPI.Repository
{
    public class UserManagerService : IUserManagerService<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<byte[]> CreateSecurityTokenAsync(ApplicationUser user)
        {
            return await _userManager.CreateSecurityTokenAsync(user);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email, string normalizedEmail)
        {
            return await _userManager.FindByEmailAsync(email ?? normalizedEmail);
        }


        public async Task<ApplicationUser> FindByUserName(string username, string normalizedUsername)
        {
            return await _userManager.FindByNameAsync(username ?? normalizedUsername);
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            var roles= await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
