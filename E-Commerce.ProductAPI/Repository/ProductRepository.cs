using AutoMapper;
using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.APIResponseLibrary.ConvertReturner;
using E_Commerce.APIResponseLibrary.EnumManager;
using E_Commerce.DAL.Models;
using E_Commerce.ProductAPI.Models;
using E_Commerce.ProductAPI.Repository.Infrasturcture;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _eCommerceDbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ECommerceDbContext eCommerceDbContext,IMapper mapper)
        {
            this._eCommerceDbContext = eCommerceDbContext;
            this._mapper = mapper;
        }

        public async Task SaveChangeAsync()
        {
            await _eCommerceDbContext.SaveChangesAsync();
        }

        public async Task<APIResponse> Create(CreateProductViewModal entity)
        {
            using (var _transaction = await _eCommerceDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var mapperProduct = _mapper.Map<ProductMaster>(entity);
                    mapperProduct.CreatedOn = MasterValueConvertorOrReturner.GetCurrentDateTimeNow();
                    mapperProduct.Sku = MasterValueConvertorOrReturner.GetUniqueRandomNumber();
                    if (mapperProduct.Description != null && mapperProduct.Description.Length > (int)UniversalEnumManager.SubStringLengthThree)
                        mapperProduct.Description = MasterValueConvertorOrReturner.GetSubStringValue(mapperProduct.Description);
                    mapperProduct.UniqueProductId = MasterValueConvertorOrReturner.Product + MasterValueConvertorOrReturner.GetUniqueRandomStringValue();

                    await _eCommerceDbContext.ProductMasters.AddAsync(mapperProduct);
                    await SaveChangeAsync();
                    await _transaction.CommitAsync();
                    var getProductViewMapper = _mapper.Map<GetProductViewModel>(mapperProduct);
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = getProductViewMapper };
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
            var getProducts = await _eCommerceDbContext.ProductMasters.ToListAsync();
            var productsMapper = _mapper.Map<List<GetProductViewModelList>>(getProducts);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = productsMapper };
        }

        public async Task<APIResponse> GetById(int id)
        {
            if (id==0)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.BadRequest, Status = false, Message = APIMessage.NotNull };
            var getProduct = await _eCommerceDbContext.ProductMasters.FirstOrDefaultAsync(x => x.Id == id);
            if (getProduct == null)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.NotFound };
            var mapperProduct = _mapper.Map<GetProductViewModel>(getProduct);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = true, Message = APIMessage.NotFound, Response = mapperProduct };
        }

        public async Task<APIResponse> GetByUniqueIdentifier(string uniqueIdentifierId)
        {
            if (string.IsNullOrEmpty(uniqueIdentifierId))
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.BadRequest, Status = false, Message = APIMessage.NotNull };
            var getProduct = await _eCommerceDbContext.ProductMasters.FirstOrDefaultAsync(x => x.UniqueProductId == uniqueIdentifierId);
            if (getProduct == null)
                return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = false, Message = APIMessage.NotFound };
            var mapperProduct = _mapper.Map<GetProductViewModel>(getProduct);
            return new APIResponse { StatusCode = (int)APIResponseEnumManager.NotFound, Status = true, Message = APIMessage.NotFound, Response = mapperProduct };
        }

        public async Task<APIResponse> TblUpdateAsync(UpdateProductViewModal updateDto)
        {
            using (var _transaction = await _eCommerceDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var mapperProduct = _mapper.Map<ProductMaster>(updateDto);
                    mapperProduct.CreatedOn = MasterValueConvertorOrReturner.GetCurrentDateTimeNow();
                    mapperProduct.Sku = MasterValueConvertorOrReturner.GetUniqueRandomNumber();
                    if (mapperProduct.Description != null && mapperProduct.Description.Length > (int)UniversalEnumManager.SubStringLengthThree)
                        mapperProduct.Description = MasterValueConvertorOrReturner.GetSubStringValue(mapperProduct.Description);
                    mapperProduct.UniqueProductId = MasterValueConvertorOrReturner.Product + MasterValueConvertorOrReturner.GetUniqueRandomStringValue();

                    await _eCommerceDbContext.ProductMasters.AddAsync(mapperProduct);
                    await SaveChangeAsync();
                    await _transaction.CommitAsync();
                    var getProductViewMapper = _mapper.Map<GetProductViewModel>(mapperProduct);
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.Success, Status = true, Message = APIMessage.Success, Response = getProductViewMapper };
                }
                catch (Exception ex)
                {
                    await _transaction.RollbackAsync();
                    return new APIResponse { StatusCode = (int)APIResponseEnumManager.InternalServerError, Status = false, Message = APIMessage.Failed, Response = ex.Message };
                }
            }
        }
    }
}
