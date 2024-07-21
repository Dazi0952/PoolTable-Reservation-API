using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AppRepository
{
    public class PoolTableRepository : IPoolTableRepository
    {
        private readonly TaskDbContext _context;

        public PoolTableRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PoolTable>> GetAllAsync()
        {
            return await _context.PoolTables.ToListAsync();
        }

        public async Task<PoolTable> GetByIdAsync(int id)
        {
            return await _context.PoolTables.FindAsync(id);
        }

        public async Task AddAsync(PoolTable pooltable)
        {
            await _context.PoolTables.AddAsync(pooltable);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PoolTable pooltable)
        {
            _context.PoolTables.Update(pooltable);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PoolTable pooltable)
        {
            _context.PoolTables.Remove(pooltable);
            await _context.SaveChangesAsync();
        }
    }
}
