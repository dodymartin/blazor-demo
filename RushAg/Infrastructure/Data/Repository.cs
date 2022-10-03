using Microsoft.EntityFrameworkCore;
using RushAg.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Infrastructure.Data
{
    public class Repository
    {
        private readonly RushAgDbContext _dbContext;

        public Repository(RushAgDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoItem> AddTodo(TodoItem entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TodoItem> Update(TodoItem entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(TodoItem entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _dbContext.TodoItems.Include(t => t.Steps).ToListAsync();
        }

        public async Task<TodoItem?> GetById(long id)
        {
            return await _dbContext.TodoItems.Where(t => t.TodoItemId == id).FirstOrDefaultAsync();
        }
    }
}
