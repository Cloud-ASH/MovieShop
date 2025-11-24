using System.Linq.Expressions;
using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MovieShopDbContext _dbContext;

    public Repository(MovieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbContext.Set<T>().Where(filter).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetCountAsync(Expression<Func<T, bool>>? filter = null)
    {
        if (filter != null)
        {
            return await _dbContext.Set<T>().Where(filter).CountAsync();
        }
        return await _dbContext.Set<T>().CountAsync();
    }

    public async Task<bool> GetExistsAsync(Expression<Func<T, bool>>? filter = null)
    {
        if (filter == null)
        {
            return await _dbContext.Set<T>().AnyAsync();
        }
        return await _dbContext.Set<T>().AnyAsync(filter);
    }
}
