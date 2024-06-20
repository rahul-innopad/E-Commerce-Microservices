using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.AuthAPI.Data;
using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Repository.AuthConfig;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.AuthAPI.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly IUserManagerService<ApplicationUser> _userManagerService;
        private readonly IPasswordHasher<ApplicationUser> _passwordManager;
        private readonly IJWTTokenGenerator _jWTTokenGenerator;
        public UserLoginRepository(IUserManagerService<ApplicationUser> userManagerService,
            IPasswordHasher<ApplicationUser> passwordManager, IJWTTokenGenerator jWTTokenGenerator)
        {
            this._userManagerService = userManagerService;
            this._passwordManager = passwordManager;
            this._jWTTokenGenerator = jWTTokenGenerator;
        }
        public async Task<APIResponse> GetLoggedInUser(string emailAddress, string password)
        {
            try
            {
                var user = await _userManagerService.FindByEmailAsync(emailAddress, emailAddress.ToUpper());
                if (user == null)
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.UserNotFound };
                else
                {
                    //Verify Password 
                    var verifyPassword = _passwordManager.VerifyHashedPassword(user, user.PasswordHash, password);
                    switch (verifyPassword)
                    {
                        case PasswordVerificationResult.Success:
                            var roles = await _userManagerService.GetUserRolesAsync(user);
                            var token = _jWTTokenGenerator.GenerateToken(user, roles);
                            return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = token };
                        case PasswordVerificationResult.Failed:
                            return new APIResponse { StatusCode = (int)APIResponseEnumManager.Failed, Status = false, Message = APIMessage.InvalidPassword };
                        case PasswordVerificationResult.SuccessRehashNeeded:
                            return new APIResponse { StatusCode = (int)APIResponseEnumManager.Failed, Status = false, Message = APIMessage.InvalidPassword };
                        default:
                            return new APIResponse { StatusCode = (int)APIResponseEnumManager.InternalServerError, Status = false, Message = APIMessage.InternalServerError };
                    }
                }
            }
            catch(Exception ex)
            {
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.InternalServerError, Status = false, Message = APIMessage.InternalServerError };
            }
           
        }

        public Task<APIResponse> GetLoggedOutUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
