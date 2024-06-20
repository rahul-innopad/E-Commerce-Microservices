using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.AuthAPI.Models.Dto;

namespace E_Commerce.AuthAPI.Repository.Infrasturcture
{
    public interface IUserRepository
    {
        Task<APIResponse> CreateUserAsync(RegisteredUserDto user);
        Task<APIResponse> GetUserListAsync();
        Task<APIResponse> GetUserAsync(string email, string username, string mobile);
        Task<APIResponse> UpdateUserAsync(RegisteredUserDto registeredUserDto);
        Task<APIResponse> DeleteUserAsync(string email, string username);
        Task SaveChangeAsync();
    }
}
