using E_Commerce.APIResponseLibrary.Constant.RoleManager;
using E_Commerce.ProductAPI.Models;
using E_Commerce.ProductAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = MasterRoleManager.AdminOrSuperAdmin)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductViewModal createProductViewModal)
        {
            var result = await _productRepository.Create(createProductViewModal);
            return Ok(result);
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productRepository.GetAllList();
            return Ok(result);
        }

    }
}
