using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.AuthAPI.Data;
using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Repository.AuthConfig;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                if (user == null || user.IsDeleted==true || user.IsActive == false)
                {
                    if(user==null)
                        return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.UserNotFound };
                    else if(user.IsDeleted)
                        return new APIResponse { StatusCode = (int)APIResponseEnumManager.IsDeleted, Status = false, Message = APIMessage.Deleted };
                    else
                        return new APIResponse { StatusCode = (int)APIResponseEnumManager.IsDeactivate, Status = false, Message = APIMessage.IsDeactivate };
                }
                else
                {
                    //Verify Password 
                    var verifyPassword = _passwordManager.VerifyHashedPassword(user, user.PasswordHash, password);
                    switch (verifyPassword)
                    {
                        case PasswordVerificationResult.Success:
                            user.IsLogin = true;
                            await _userManagerService.UpdateAsync(user);
                            await _userManagerService.SaveChangeAsync();
                            var roles = await _userManagerService.GetUserRolesAsync(user);
                            var token = _jWTTokenGenerator.GenerateToken(user, roles,roles.First());
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

        public async Task<APIResponse> GetLoggedOutUser(string emailId)
        {
            var getUser = await _userManagerService.FindByEmailAsync(emailId,emailId.ToUpper());
            if(getUser==null)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.UserNotFound };
            else
            {
                getUser.IsLogin = false;
                await _userManagerService.UpdateAsync(getUser);
                await _userManagerService.SaveChangeAsync();
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.LoggedOut };
            }
        }
    }
}
