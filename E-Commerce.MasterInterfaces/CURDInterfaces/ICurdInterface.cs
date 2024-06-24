using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.MasterInterfaces.CURDInterfaces
{
    public interface ICurdInterface<TAPIResponses,TTableCreateDto, TUpdateDto>
    {
        Task<TAPIResponses> Create(TTableCreateDto entity);
        Task<TAPIResponses> GetAllList();
        Task<TAPIResponses> GetById(int id);
        Task<TAPIResponses> GetByUniqueIdentifier(string uniqueIdentifierId);
        Task<TAPIResponses> TblUpdateAsync(TUpdateDto updateDto);
    }
}
