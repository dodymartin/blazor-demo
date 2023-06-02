using BlazorDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Core.Interfaces
{
    public interface IRepository
    {
        public Task<TodoItem?> GetByIdAsync(long id);
        public Task<IEnumerable<TodoItem>> GetAllAsync();
        public Task<TodoItem> AddAsync(TodoItem entity);
        public Task<TodoItem> UpdateAsync(TodoItem entity);
        public Task DeleteAsync(TodoItem entity);
    }
}
