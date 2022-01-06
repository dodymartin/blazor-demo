using RushAg.Shared;

namespace RushAg.Server.Data;

public class DataRepository : IDataRepository
{
    //TODO Need an interface for the dbContext
    private readonly RushAgDbContext _db;

    public DataRepository(RushAgDbContext db)
    {
        _db = db;
    }
    public TodoItem Add(TodoItem item)
    {
        _db.TodoItems.Add(item);
        return item;
    }

    public void Delete(TodoItem item)
    {
        _db.TodoItems.Remove(item);
    }

    public IEnumerable<TodoItem> GetAll()
    {
        return _db.TodoItems;
    }

    public TodoItem? GetById(long id)
    {
        return _db.TodoItems.FirstOrDefault(x => x.Id == id);
    }

    public TodoItem Update(TodoItem item)
    {
        _db.TodoItems.Update(item);
        return item;
    }

    public bool Save()
    {
        return (_db.SaveChanges() >= 0);
    }
}
