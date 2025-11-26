using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSIA.Core.Application.Interfaces.Services
{
    public interface IGenericService<TSaveDto, TDto, TEntity>
        where TEntity : class
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TSaveDto dto);
        Task<TDto> UpdateAsync(int id, TSaveDto dto);
        Task DeleteAsync(int id);
    }
}
