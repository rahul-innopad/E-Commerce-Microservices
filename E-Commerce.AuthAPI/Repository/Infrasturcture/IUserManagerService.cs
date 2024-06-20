using Microsoft.AspNetCore.Identity;

namespace E_Commerce.AuthAPI.Repository.Infrasturcture
{
    public interface IUserManagerService<T>
    {
        Task<IdentityResult> CreateAsync(T user);
        Task<IdentityResult> UpdateAsync(T user);
        Task<T> FindByEmailAsync(string email, string normalizedEmail);
        Task<T> FindByUserName(string username, string normalizedUsername);
        Task<IList<string>> GetUserRolesAsync(T user);
        Task<byte[]> CreateSecurityTokenAsync(T user);
        Task SaveChangeAsync();
    }
}
