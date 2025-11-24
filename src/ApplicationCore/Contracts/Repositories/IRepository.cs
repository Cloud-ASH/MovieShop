using System.Linq.Expressions;

namespace ApplicationCore.Contracts.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> GetCountAsync(Expression<Func<T, bool>>? filter = null);
    Task<bool> GetExistsAsync(Expression<Func<T, bool>>? filter = null);
}
