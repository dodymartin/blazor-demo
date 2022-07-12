

namespace RushAg.Infrastructure.Data;

public interface IDataRepository
{
    IEnumerable<TodoItem> GetAll();
    TodoItem? GetById(long id);
    TodoItem Add(TodoItem item);
    TodoItem Update(TodoItem item);
    void Delete(TodoItem item);

    TodoStep Add(TodoStep item);
    bool Save();
}
