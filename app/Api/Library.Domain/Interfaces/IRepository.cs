using System.Linq.Expressions;
using Library.Domain.Entities.Base;

namespace Library.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> Insert(T entity);
    Task<T> GetById(Guid id);
    Task<List<T>> GetAll(
        Expression<Func<T, bool>> filter = null!,
        bool paginate = true,
        int page = 1,
        int pageSize = 10,
        string orderBy = "desc");
    Task<int> Count(Expression<Func<T, bool>> filter = null!);
    Task<T> FindOne(
        Expression<Func<T, bool>> filter);
    Task<T> Update(T entity);
    Task Delete(Guid id);
}