using AutoMapper;
using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.AuthAPI.Data;
using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Models.Dto;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.AuthAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IUserManagerService<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordManager;
        public UserRepository(ApplicationDbContext applicationDbContext,IMapper mapper,
            IUserManagerService<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordManager)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._userManager = userManager;
            this._passwordManager = passwordManager;
        }

        public async Task<APIResponse> CreateUserAsync(RegisteredUserDto user)
        {
            var createUserMapper = _mapper.Map<ApplicationUser>(user);
            createUserMapper.PasswordHash = _passwordManager.HashPassword(createUserMapper,createUserMapper.PasswordHash);
            var result = await _userManager.CreateAsync(createUserMapper);
            await SaveChangeAsync();
            if (result.Succeeded)
            {
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = result };
            }
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.InternalServerError, Status = false, Message = APIMessage.Failed, Response = result };
        }

        public async Task<APIResponse> GetUserAsync(string email, string username, string mobile)
        {
            if(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(username) && string.IsNullOrEmpty(mobile))
            {
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.BadRequest, Status = false, Message = APIMessage.AtLeastProvideOne };
            }
            var getUser = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(x=> (x.Email==email && x.NormalizedEmail==email.ToUpper()) || (x.UserName==username && x.UserName==username.ToUpper()) || x.PhoneNumber==mobile);
            if (getUser==null)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.UserNotFound };
            var userMapper = _mapper.Map<UserDto>(getUser);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = userMapper };
        }

        public async Task<APIResponse> GetUserListAsync()
        {
            var userList = await _applicationDbContext.ApplicationUsers.ToListAsync();
            var userListMapper = _mapper.Map<List<UserDto>>(userList);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = userListMapper };

        }

        public async Task SaveChangeAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<APIResponse> UpdateUserAsync(RegisteredUserDto registeredUserDto)
        {
            var gerUser = await _applicationDbContext.ApplicationUsers.FirstOrDefaultAsync(x => (x.Email == registeredUserDto.EmailAddress && x.NormalizedEmail == registeredUserDto.EmailAddress.ToUpper()) || x.PhoneNumber == registeredUserDto.Phone);
            if(gerUser==null)
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.UserNotFound };
            else
            {
                var userMapper = _mapper.Map<RegisteredUserDto, ApplicationUser>(registeredUserDto, gerUser);
                userMapper.PasswordHash = _passwordManager.HashPassword(userMapper, userMapper.PasswordHash);
                var result=await _userManager.UpdateAsync(userMapper);
                await SaveChangeAsync();
                if(result.Succeeded)
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success ,Response=result};
                else
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.InternalServerError, Status = false, Message = APIMessage.Failed };
            }
        }
    }
}
