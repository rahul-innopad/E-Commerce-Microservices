using AutoMapper;
using E_Commerce.CategoryAPI.Models;
using E_Commerce.DAL.Models;

namespace E_Commerce.CategoryAPI.Utility
{
    public class MasterMappingConfig : Profile
    {
        public MasterMappingConfig()
        {
            CreateMap<UpdateCategoryViewModel, CategoryMaster>();
            CreateMap<CreateCategoryViewModel, CategoryMaster>();
        }
    }
}
