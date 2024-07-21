using Application.Interfaces;
using Core.Interfaces;
using Application.DTOs;
using Core.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PoolTableService : IPoolTableService
    {
        private readonly IPoolTableRepository _pooltableRepository;

        public PoolTableService(IPoolTableRepository pooltableRepository)
        {
            _pooltableRepository = pooltableRepository;
        }

        public async Task<PoolTableDto> GetPoolTableByIdAsync(int id)
        {
            var pooltable = await _pooltableRepository.GetByIdAsync(id);
            if (pooltable == null)
            {
                return null;
            }
            return new PoolTableDto { Id = pooltable.Id, Name = pooltable.Name };
        }

        public async Task<IEnumerable<PoolTableDto>> GetAllPoolTablesAsync()
        {
            var pooltables = await _pooltableRepository.GetAllAsync();
            return pooltables.Select(c => new PoolTableDto { Id = c.Id, Name = c.Name });
        }

        public async Task AddPoolTableAsync(CreatePoolTableDto pooltableDto)
        {
            var pooltable = new PoolTable
            {
                Id = pooltableDto.Id,
                Name = pooltableDto.Name
            };
            await _pooltableRepository.AddAsync(pooltable);
        }

        public async Task UpdatePoolTableAsync(UpdatePoolTableDto pooltableDto)
        {
            var pooltable = await _pooltableRepository.GetByIdAsync(pooltableDto.Id);
            if (pooltable != null)
            {
                pooltable.Name = pooltableDto.Name;
                await _pooltableRepository.UpdateAsync(pooltable);
            }
        }

        public async Task DeletePoolTableAsync(int id)
        {
            var pooltable = await _pooltableRepository.GetByIdAsync(id);
            if (pooltable != null)
            {
                await _pooltableRepository.DeleteAsync(pooltable);
            }
        }
    }
}
