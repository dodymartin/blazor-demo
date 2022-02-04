using RushAg.Shared;

namespace RushAg.Server.Data
{
    public interface IDataRepository
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(long id);
        TodoItem Add(TodoItem item);
        TodoItem Update(TodoItem item);
        void Delete(TodoItem item);
        bool Save();
    }
}
