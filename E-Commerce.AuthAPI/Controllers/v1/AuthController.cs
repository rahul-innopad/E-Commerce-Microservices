using E_Commerce.APIResponseLibrary.Constant.RoleManager;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.AuthAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public AuthController(IUserLoginRepository userLoginRepository)
        {
            this._userLoginRepository = userLoginRepository;
        }

        [AllowAnonymous]
        [HttpPost("AuthLogin")]
        public async Task<IActionResult> AuthLogin(string emailAddress, string password)
        {
            var result = await _userLoginRepository.GetLoggedInUser(emailAddress, password);
            return Ok(result);
        }

        [Authorize(Roles = MasterRoleManager.AdminOrSuperAdminOrUser)]
        [HttpPost("Logout")]
        public async Task<IActionResult> LoggedOutAuth()
        {
            var getUserId = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            if (getUserId != null)
            {
                var result = await _userLoginRepository.GetLoggedOutUser(getUserId);
                return Ok(result);
            }
            return Ok();
        }
    }
}
