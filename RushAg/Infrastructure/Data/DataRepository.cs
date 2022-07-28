//using Microsoft.EntityFrameworkCore;

//namespace RushAg.Infrastructure.Data;

//public class DataRepository : IDataRepository
//{
//    //TODO Need an interface for the dbContext
//    private readonly RushAgDbContext _db;

//    public DataRepository(RushAgDbContext db)
//    {
//        _db = db;
//    }
//    public TodoItemDto Add(TodoItemDto item)
//    {
//        _db.TodoItems.Add(item);
//        return item;
//    }

//    public void Delete(TodoItemDto item)
//    {
//        _db.TodoItems.Remove(item);
//    }

//    public IEnumerable<TodoItemDto> GetAll()
//    {
//        return _db.TodoItems
//            .Include(t => t.Steps)
//            .AsNoTracking()
//            .ToList();
//    }

//    public TodoItemDto? GetById(long id)
//    {
//        return _db.TodoItems
//            .Include(t => t.Steps)
//            .AsNoTracking()
//            .FirstOrDefault(x => x.TodoItemId == id);
//    }

//    public TodoItemDto Update(TodoItemDto item)
//    {
//        if (item.IsComplete)
//            item.CompleteDate = DateTime.Now;
//        else
//            item.CompleteDate = null;

//        _db.TodoItems.Update(item);
//        return item;
//    }

//    public TodoStep Add(TodoStep item)
//    {
//        _db.TodoSteps.Add(item);
//        return item;
//    }

//    public bool Save()
//    {
//        return (_db.SaveChanges() >= 0);
//    }
//}
