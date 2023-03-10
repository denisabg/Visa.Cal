using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Visa.Cal.Abstraction.Data;
using Visa.Cal.Abstraction.Domain;

namespace Visa.Cal.Data;

public class RepositoryGeneric<T> :
    IRepositoryGeneric<T> where T : class, IHasId, new()
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public RepositoryGeneric(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddOrUpdateAsync(T item, CancellationToken cancellationToken = default)
    {
        var entity = _dbSet.Attach(entity: item);

        entity.State = item.Id <= 0 ? EntityState.Added : EntityState.Modified;

        await _context.SaveChangesAsync(cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(T item, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity: item);

        await _context.SaveChangesAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<T>> FetchAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var items = await _dbSet
            .Where(predicate: predicate)
            .ToArrayAsync(cancellationToken: cancellationToken);

        return items;
    }
}