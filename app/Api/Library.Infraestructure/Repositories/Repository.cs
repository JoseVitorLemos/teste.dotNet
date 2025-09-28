using System.Linq.Expressions;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities.Base;
using Library.Infraestructure.AppDbContext;
using Library.Domain.Messages;

namespace Library.Infraestructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    public readonly DbSet<T> _dbSet;
    public readonly DataContext _dbContext;

    public Repository(DataContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbContext = dbContext;
    }

    public async Task<T> Insert(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetById(Guid id)
    {
        var entity = await _dbSet
            .AsNoTracking().AsQueryable()
            .FirstOrDefaultAsync(e => e.Id == id);

        return entity!;
    }

    public async Task<List<T>> GetAll(
        Expression<Func<T, bool>> filter = null!,
        bool paginate = true,
        int page = 1,
        int pageSize = 10,
        string orderBy = "desc")
    {
        var query = _dbSet.AsQueryable();

        query = orderBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
            ? query.OrderBy(x => x.Id)
            : query.OrderByDescending(x => x.Id);

        if (page < 1) page = 1;
        if (paginate)
            query = query.Skip((page - 1) * pageSize)
                         .Take(pageSize);

        if (filter != null)
            query = query.Where(filter)
                         .AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<int> Count(Expression<Func<T, bool>> filter = null!)
    {
        if (filter != null)
            return await _dbSet.CountAsync(filter);

        return await _dbSet.CountAsync();
    }

    public async Task<T> FindOne(Expression<Func<T, bool>> filter)
    {
        var entity = await _dbSet.AsQueryable()
            .AsNoTracking()
            .AsQueryable()
            .FirstOrDefaultAsync(filter);

        return entity!;
    }

    public async Task<T> Update(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);

        if (entity is null)
            throw new ArgumentException(string.Format(EntityMessages.EMPTY, typeof(T).Name, $"id {id}"));

        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}