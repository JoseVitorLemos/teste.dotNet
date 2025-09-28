using System.Linq.Expressions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities.Base;
using Library.Infraestructure.AppDbContext;

namespace Library.Infraestructure.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(DataContext dbContext) : base(dbContext)
    { }

    public async Task<List<Book>> GetBookOrderByName(Expression<Func<Book, bool>> filter = null!,
        bool paginate = true,
        int page = 1,
        int pageSize = 10,
        string orderBy = "desc")
    {
        var query = _dbSet.AsQueryable();

        if (filter != null)
            query = query.Where(filter)
                         .AsNoTracking();

        query = orderBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
            ? query.OrderBy(x => x.Name)
            : query.OrderByDescending(x => x.Name);

        if (page < 1) page = 1;
        if (paginate)
            query = query.Skip((page - 1) * pageSize)
                         .Take(pageSize);

        return await query.ToListAsync();
    }
}