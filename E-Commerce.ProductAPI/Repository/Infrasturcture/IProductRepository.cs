using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.MasterInterfaces.CURDInterfaces;
using E_Commerce.ProductAPI.Models;

namespace E_Commerce.ProductAPI.Repository.Infrasturcture
{
    public interface IProductRepository : ICurdInterface<APIResponse , CreateProductViewModal, UpdateProductViewModal>
    {
        Task SaveChangeAsync();
    }
}
