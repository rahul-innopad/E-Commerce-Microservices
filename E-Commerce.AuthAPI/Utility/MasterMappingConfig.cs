using AutoMapper;
using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Models.Dto;

namespace E_Commerce.AuthAPI.Utility
{
    public class MasterMappingConfig : Profile
    {
        public MasterMappingConfig()
        {
            //Create Mapping For User
            CreateMap<RegisteredUserDto, ApplicationUser>().ForMember(x =>
                x.UserName, src => src.MapFrom(x => x.Username)).ForMember(x =>
                x.NormalizedUserName, src => src.MapFrom(x => x.Username.ToUpper())).ForMember(x =>
                x.Email, src => src.MapFrom(x => x.EmailAddress)).ForMember(x =>
                x.NormalizedEmail, src => src.MapFrom(x => x.EmailAddress.ToUpper())).ForMember(x =>
                x.PasswordHash, src => src.MapFrom(x => x.Password)).ForMember(x =>
                x.PhoneNumber, src => src.MapFrom(x => x.Phone));

            CreateMap<ApplicationUser, UserDto>().ForMember(x =>
                x.Username, src => src.MapFrom(x => x.UserName)).ForMember(x =>
                x.EmailAddress, src => src.MapFrom(x => x.Email)).ForMember(x =>
                x.Phone, src => src.MapFrom(x => x.PhoneNumber)).ReverseMap();
        }
    }
}
