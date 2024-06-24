using AutoMapper;
using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.APIResponseLibrary.ConvertReturner;
using E_Commerce.APIResponseLibrary.EnumManager;
using E_Commerce.CategoryAPI.Models;
using E_Commerce.CategoryAPI.Repository.Infrasturcture;
using E_Commerce.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.CategoryAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceDbContext _eCommerceDbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(ECommerceDbContext eCommerceDbContext,IMapper mapper)
        {
            this._eCommerceDbContext = eCommerceDbContext;
            this._mapper = mapper;
        }

        public async Task<APIResponse> Create(CreateCategoryViewModel entity)
        {
            using (var _transaction = await _eCommerceDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var mapperCategory = _mapper.Map<CategoryMaster>(entity);
                    mapperCategory.CreatedOn = MasterValueConvertorOrReturner.GetCurrentDateTimeNow();
                    if (mapperCategory.Description != null && mapperCategory.Description.Length > (int)UniversalEnumManager.SubStringLengthThree)
                        mapperCategory.Description = MasterValueConvertorOrReturner.GetSubStringValue(mapperCategory.Description);
                    mapperCategory.UniqueCategoryId = MasterValueConvertorOrReturner.Category + MasterValueConvertorOrReturner.GetUniqueRandomStringValue();

                    await _eCommerceDbContext.CategoryMasters.AddAsync(mapperCategory);
                    await SaveChangeAsync();
                    await _transaction.CommitAsync();
                    var getCategoryViewMapper = _mapper.Map<GetCategoryViewModel>(mapperCategory);
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = getCategoryViewMapper };
                }
                catch (Exception ex)
                {
                    await _transaction.RollbackAsync();
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.InternalServerError, Status = false, Message = APIMessage.Failed, Response = ex.Message };
                }
            }
        }
        
        public async Task<APIResponse> GetAllList()
        {
            var getAllCategory = await _eCommerceDbContext.CategoryMasters.ToListAsync();
            var mapperCategory = _mapper.Map<List<CreateCategoryViewModel>>(getAllCategory);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = mapperCategory };
        }

        public async Task<APIResponse> GetById(int id)
        {
            if (id == 0)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.BadRequest, Status = false, Message = APIMessage.NotNull };
            var getCategory = await _eCommerceDbContext.CategoryMasters.FirstOrDefaultAsync(x => x.Id == id);
            if(getCategory==null)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.NotFound };
            var mapperCategory = _mapper.Map<GetCategoryViewModel>(getCategory);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = true, Message = APIMessage.NotFound, Response= mapperCategory };
        }

        public async Task<APIResponse> GetByUniqueIdentifier(string uniqueIdentifierId)
        {
            if (string.IsNullOrEmpty(uniqueIdentifierId))
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.BadRequest, Status = false, Message = APIMessage.NotNull };
            var getCategory = await _eCommerceDbContext.CategoryMasters.FirstOrDefaultAsync(x => x.UniqueCategoryId == uniqueIdentifierId);
            if (getCategory == null)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.NotFound };
            var mapperCategory = _mapper.Map<GetCategoryViewModel>(getCategory);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = true, Message = APIMessage.NotFound, Response = mapperCategory };
        }

        public async Task<APIResponse> TblUpdateAsync(UpdateCategoryViewModel updateDto)
        {
            using (var _transaction = await _eCommerceDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var getCategory = await _eCommerceDbContext.CategoryMasters.FirstOrDefaultAsync(x => x.UniqueCategoryId.Equals(updateDto.UniqueCategoryId) || x.Id.Equals(updateDto.Id));
                    if(getCategory==null)
                        return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.NotFound };
                    var mapperCategory = _mapper.Map<CategoryMaster>(updateDto);
                    mapperCategory.LastUpdateAt = MasterValueConvertorOrReturner.GetCurrentDateTimeNow();
                    if (mapperCategory.Description != null && mapperCategory.Description.Length > (int)UniversalEnumManager.SubStringLengthThree)
                        mapperCategory.Description = MasterValueConvertorOrReturner.GetSubStringValue(mapperCategory.Description);
                    _eCommerceDbContext.CategoryMasters.Update(mapperCategory);
                    await SaveChangeAsync();
                    await _transaction.CommitAsync();
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success };
                }
                catch (Exception ex)
                {
                    await _transaction.RollbackAsync();
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.InternalServerError, Status = false, Message = APIMessage.Failed, Response = ex.Message };
                }
            }
        }
        public async Task SaveChangeAsync()
        {
            await _eCommerceDbContext.SaveChangesAsync();
        }

    }
}
