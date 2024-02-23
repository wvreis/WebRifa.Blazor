namespace WebRifa.Blazor.Core.Interfaces.Repositories;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<Guid> AddAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task DeleteRangeAsync(List<T> entity, CancellationToken cancellationToken);
    Task<T> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> EntityExistsAsync(Guid entityId, CancellationToken cancellationToken);
}

