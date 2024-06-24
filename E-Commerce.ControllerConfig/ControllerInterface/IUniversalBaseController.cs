using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.ControllerConfig.ControllerInterface
{
    public interface IUniversalBaseController<TActionResult, TTableCreateDto , TUpdateDto >
    {
        Task<TActionResult> CreateAsync(TTableCreateDto entity);
        Task<TActionResult> GetAllListAsync();
        Task<TActionResult> GetByIdAsync(int id);
        Task<TActionResult> GetByUniqueIdentifierAsync(string uniqueIdentifierId);
        Task<TActionResult> UpdateAsync(TUpdateDto updateDto);
        Task<TActionResult> GetActiveAsync();
    }
}
