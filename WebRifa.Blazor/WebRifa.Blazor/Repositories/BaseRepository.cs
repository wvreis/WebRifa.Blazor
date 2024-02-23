using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Common;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Core.Repositories;
public class BaseRepository<T>(
    ApplicationDbContext context) : IBaseRepository<T> where T : BaseEntity {
    protected readonly ApplicationDbContext _context = context;

    public virtual async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken)
    {        
        await _context.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public virtual async Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        if (!entities.Any()) {
            throw new InvalidOperationException("A lista não contém elementos.");
        }

        await _context.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        if (!await EntityExistsAsync(entity.Id, cancellationToken)) {
            throw new KeyNotFoundException($"Entidade do Tipo {typeof(T).Name} com Id {entity.Id} não existe.");
        }

        entity.SetUpdatedAt();
        _context.Update(entity);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        if (!await EntityExistsAsync(entity.Id, cancellationToken)) {
            throw new KeyNotFoundException($"Entidade do Tipo {typeof(T).Name} com Id {entity.Id} não existe." );
        }

        entity.MarkAsDeleted();
        _context.Update(entity);
    }

    public virtual async Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        if (!entities.Any()) {
            throw new InvalidOperationException("A lista não contém elementos.");
        }

        var entitiesIds = entities
            .Select(e => e.Id)
            .ToList();

        var entitiesIdsOnDatabase = _context.Set<T>()
            .Where(t => entitiesIds
                .Contains(t.Id))
                .Select(t => t.Id)
            .ToList();

        bool allEntitiesExist = entitiesIds
            .All(eId => entitiesIdsOnDatabase.Contains(eId));

        if (!allEntitiesExist) {
            throw new KeyNotFoundException($"Todas as entidades da lista devem existir no banco de dados.");
        }

        await _context.Set<T>()
            .Where(t => entities.Select(ent => ent.Id).Contains(t.Id))
            .ExecuteUpdateAsync(setPropCall => setPropCall.SetProperty(p => p.IsDeleted, true));
    }

    public virtual async Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result ?? throw new NullReferenceException($"Entidade com Id {id} não encontrada.");
    }

    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual Task<bool> EntityExistsAsync(Guid entityId, CancellationToken cancellationToken)
    {
        return _context.Set<T>().AnyAsync(e => e.Id == entityId);
    }
}