using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.AuthAPI.Models.Dto;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.AuthAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> RegisterUser(RegisteredUserDto registeredUserDto)
        {
            var result = await _userRepository.CreateUserAsync(registeredUserDto);
            return Ok(result);
        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserListAsync()
        {
            var result = await _userRepository.GetUserListAsync();
            return Ok(result);
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUserAsync(string? email, string? username, string? mobile)
        {
            var result = await _userRepository.GetUserAsync(email, username, mobile);
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(RegisteredUserDto registeredUserDto)
        {
            var result = await _userRepository.UpdateUserAsync(registeredUserDto);
            return Ok(result);
        }
    }
}
