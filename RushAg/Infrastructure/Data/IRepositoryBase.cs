
using RushAg.Shared;

namespace RushAg.Infrastructure.Data;

public interface IRepositoryBase<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(long id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
}
