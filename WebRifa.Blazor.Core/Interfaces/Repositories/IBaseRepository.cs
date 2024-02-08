namespace WebRifa.Blazor.Core.Interfaces.Repositories;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
    void Update(T entity);
    void Delete(T entity);
    Task<T> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
}

