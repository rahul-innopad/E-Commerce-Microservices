using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.CategoryAPI.Models;
using E_Commerce.MasterInterfaces.CURDInterfaces;

namespace E_Commerce.CategoryAPI.Repository.Infrasturcture
{
    public interface ICategoryRepository : ICurdInterface<APIResponse, CreateCategoryViewModel, UpdateCategoryViewModel>
    {
        Task SaveChangeAsync();
    }
}
