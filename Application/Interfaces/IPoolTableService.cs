using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPoolTableService
    {
        Task<PoolTableDto> GetPoolTableByIdAsync(int id);
        Task<IEnumerable<PoolTableDto>> GetAllPoolTablesAsync();
        Task AddPoolTableAsync(CreatePoolTableDto pooltableDto);
        Task UpdatePoolTableAsync(UpdatePoolTableDto pooltableDto);
        Task DeletePoolTableAsync(int id);
    }
}
