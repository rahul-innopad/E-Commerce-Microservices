using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("AuthLogin")]
        public async Task<IActionResult> AuthLogin(string emailAddress, string password)
        {
            var result = await _userLoginRepository.GetLoggedInUser(emailAddress, password);
            return Ok(result);
        }
    }
}
