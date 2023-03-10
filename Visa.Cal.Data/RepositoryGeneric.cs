using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Visa.Cal.Abstraction.Data;
using Visa.Cal.Abstraction.Domain;

namespace Visa.Cal.Data;

public class RepositoryGeneric<T> : 
    IRepositoryGeneric<T> where T : class, IHasId, new()
{
    private readonly DbContext _context;

    public RepositoryGeneric(DbContext context)
    {
        _context = context;
    }
    
    public async Task AddOrUpdateAsync(T item, CancellationToken cancellationToken  = default)
    {
        var dbSet = _context.Set<T>();

        var entity = dbSet.Attach(item);

        entity.State = item.Id <= 0 ? EntityState.Added : EntityState.Modified; 

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T item, CancellationToken cancellationToken = default)
    {
        var dbSet = _context.Set<T>();

        dbSet.Remove(item);
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<T>> FetchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var dbSet = _context.Set<T>();
        
        var items = await dbSet
            .Where(predicate)
            .ToArrayAsync(cancellationToken: cancellationToken);

        return items;
    }
}