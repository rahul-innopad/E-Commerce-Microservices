using AutoMapper;
using E_Commerce.DAL.Models;
using E_Commerce.ProductAPI.Models;

namespace E_Commerce.ProductAPI.Utility
{
    public class MasterMappingConfig : Profile
    {
        public MasterMappingConfig()
        {
            CreateMap<CreateProductViewModal, ProductMaster>();
            CreateMap<ProductMaster, GetProductViewModel>();
            CreateMap<ProductMaster, GetProductViewModelList>();
            CreateMap<UpdateProductViewModal, ProductMaster>();
        }
    }
}
