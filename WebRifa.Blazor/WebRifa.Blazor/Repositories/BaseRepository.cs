using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Common;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Core.Repositories;
public class BaseRepository<T>(
    ApplicationDbContext context) : IBaseRepository<T> where T : BaseEntity {

    protected const int PageSize = 10;

    public virtual async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken)
    {        
        await context.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public virtual async Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        if (!entities.Any()) {
            throw new InvalidOperationException("A lista não contém elementos.");
        }

        await context.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        if (!await EntityExistsAsync(entity.Id, cancellationToken)) {
            throw new KeyNotFoundException($"Entidade do Tipo {typeof(T).Name} com Id {entity.Id} não existe.");
        }

        entity.SetUpdatedAt();
        context.Update(entity);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        if (!await EntityExistsAsync(entity.Id, cancellationToken)) {
            throw new KeyNotFoundException($"Entidade do Tipo {typeof(T).Name} com Id {entity.Id} não existe." );
        }

        entity.MarkAsDeleted();
        context.Update(entity);
    }

    public virtual async Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        if (!entities.Any()) {
            throw new InvalidOperationException("A lista não contém elementos.");
        }

        var entitiesIds = entities
            .Select(e => e.Id)
            .ToList();

        var entitiesIdsOnDatabase = context.Set<T>()
            .Where(t => entitiesIds
                .Contains(t.Id))
                .Select(t => t.Id)
            .ToList();

        bool allEntitiesExist = entitiesIds
            .All(eId => entitiesIdsOnDatabase.Contains(eId));

        if (!allEntitiesExist) {
            throw new KeyNotFoundException($"Todas as entidades da lista devem existir no banco de dados.");
        }

        await context.Set<T>()
            .Where(t => entities.Select(ent => ent.Id).Contains(t.Id))
            .ExecuteUpdateAsync(setPropCall => setPropCall.SetProperty(p => p.IsDeleted, true));
    }

    public virtual async Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result ?? throw new NullReferenceException($"Entidade com Id {id} não encontrada.");
    }

    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual async Task<PaginatedList<T>> GetAllAsync(int currentPage, CancellationToken cancellationToken)
    {
        PaginatedList<T> paginatedList = await GetPaginatedEntitiesAsync(currentPage, cancellationToken);

        return paginatedList;
    }

    protected async Task<PaginatedList<T>> GetPaginatedEntitiesAsync(
        int currentPage,
        CancellationToken cancellationToken,
        Expression<Func<T, bool>>? predicate = null)
    {
        List<T> items = await context.Set<T>()
            .Where(predicate!)   
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(cancellationToken);

        int totalCountItems = await context.Set<T>()
            .Where(predicate!)
            .CountAsync();

        int totalPages = (int)Math.Ceiling(
            Convert.ToDecimal(totalCountItems) / 
            Convert.ToDecimal(PageSize));

        var paginatedList = new PaginatedList<T>(currentPage, totalPages, PageSize, items);
        return paginatedList;
    }

    public virtual Task<bool> EntityExistsAsync(Guid entityId, CancellationToken cancellationToken)
    {
        return context.Set<T>().AnyAsync(e => e.Id == entityId);
    }
}