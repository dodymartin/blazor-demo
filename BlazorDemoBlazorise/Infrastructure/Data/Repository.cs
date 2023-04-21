using Microsoft.EntityFrameworkCore;
using RushAg.Core.Entities;
using RushAg.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Infrastructure.Data
{
    public class Repository : IRepository
    {
        private readonly RushAgDbContext _dbContext;

        public Repository(RushAgDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem?> GetByIdAsync(long id)
        {

            return await _dbContext.TodoItems
                .Where(t => t.Id == id)
                .Include(t => t.Steps)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _dbContext.TodoItems
                .Include(t => t.Steps)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TodoItem> AddAsync(TodoItem entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TodoItem entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
