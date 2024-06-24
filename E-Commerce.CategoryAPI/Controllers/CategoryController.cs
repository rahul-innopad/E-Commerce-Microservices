using E_Commerce.APIResponseLibrary.Constant.RoleManager;
using E_Commerce.CategoryAPI.Models;
using E_Commerce.CategoryAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = MasterRoleManager.AdminOrSuperAdmin)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateAsync(CreateCategoryViewModel entity)
        {
            var result = await _categoryRepository.Create(entity);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GetActiveCategories")]
        public async Task<IActionResult> GetActiveAsync()
        {
            var result = await _categoryRepository.GetAllList();
            return Ok(result);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllListAsync()
        {
            var result = await _categoryRepository.GetAllList();
            return Ok(result);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _categoryRepository.GetById(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("GetCategoryByUniqueIdentifierId")]
        public async Task<IActionResult> GetByUniqueIdentifierAsync(string uniqueIdentifierId)
        {
            var result = await _categoryRepository.GetByUniqueIdentifier(uniqueIdentifierId);
            return Ok(result);
        }

        [Authorize(Roles = MasterRoleManager.AdminOrSuperAdminOrUser)]
        [HttpPut("UpdateCategoryAsyncBy")]
        public async Task<IActionResult> UpdateAsync(UpdateCategoryViewModel updateDto)
        {
            var result = await _categoryRepository.TblUpdateAsync(updateDto);
            return Ok(result);
        }
    }
}
