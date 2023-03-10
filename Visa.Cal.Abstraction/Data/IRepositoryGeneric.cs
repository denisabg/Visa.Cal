using System.Linq.Expressions;
using Visa.Cal.Abstraction.Domain;

namespace Visa.Cal.Abstraction.Data;

public interface IRepositoryGeneric<T>
    where T : class, IHasId, new()
{
    Task AddOrUpdateAsync(T item, CancellationToken cancellationToken = default);
    Task DeleteAsync(T item, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<T>> FetchAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);
}