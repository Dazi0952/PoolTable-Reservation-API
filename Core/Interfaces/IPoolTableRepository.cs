using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPoolTableRepository
    {
        Task<IEnumerable<PoolTable>> GetAllAsync();
        Task<PoolTable> GetByIdAsync(int id);
        Task AddAsync(PoolTable pooltable);
        Task UpdateAsync(PoolTable pooltable);
        Task DeleteAsync(PoolTable pooltable);
    }
}
