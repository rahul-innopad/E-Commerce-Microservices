using E_Commerce.APIResponseLibrary.Constant.APIConstants;

namespace E_Commerce.AuthAPI.Repository.Infrasturcture
{
    public interface IUserLoginRepository
    {
        Task<APIResponse> GetLoggedInUser(string emailAddress, string password);
        Task<APIResponse> GetLoggedOutUser(string emailId);
    }
}
