using Microsoft.EntityFrameworkCore;

namespace RushAg.Infrastructure.Data;

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
        return _db.TodoItems
            .Include(t => t.Steps)
            .AsNoTracking()
            .ToList();
    }

    public TodoItem? GetById(long id)
    {
        return _db.TodoItems
            .Include(t => t.Steps)
            .AsNoTracking()
            .FirstOrDefault(x => x.TodoItemId == id);
    }

    public TodoItem Update(TodoItem item)
    {
        if (item.IsComplete)
            item.CompleteDate = DateTime.Now;
        else
            item.CompleteDate = null;

        _db.TodoItems.Update(item);
        return item;
    }

    public TodoStep Add(TodoStep item)
    {
        _db.TodoSteps.Add(item);
        return item;
    }

    public bool Save()
    {
        return (_db.SaveChanges() >= 0);
    }
}
