﻿using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Common;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Core.Repositories;
public class BaseRepository<T>(
    ApplicationDbContext context,
    ICustomUserIdProvider customUserIdProvider) : IBaseRepository<T> where T : BaseEntity {
    protected readonly ApplicationDbContext _context = context;
    private readonly ICustomUserIdProvider _customUserIdProvider = customUserIdProvider;

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {        
        entity.SetCreatedBy(await _customUserIdProvider.GetUserIdAsync());

        await _context.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        if (!entities.Any()) {
            throw new InvalidOperationException("A lista não contém elementos.");
        }

        entities.ForEach(async e => {
            e.SetCreatedBy(await _customUserIdProvider.GetUserIdAsync());
        });

        await _context.AddRangeAsync(entities, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        if (!await EntityExistsAsync(entity.Id, cancellationToken)) {
            throw new KeyNotFoundException($"Entidade do Tipo {typeof(T).Name} com Id {entity.Id} não existe.");
        }

        entity.SetCreatedBy(await _customUserIdProvider.GetUserIdAsync());
        entity.SetUpdatedAt();
        _context.Update(entity);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        if (!await EntityExistsAsync(entity.Id, cancellationToken)) {
            throw new KeyNotFoundException($"Entidade do Tipo {typeof(T).Name} com Id {entity.Id} não existe." );
        }

        entity.SetCreatedBy(await _customUserIdProvider.GetUserIdAsync());
        entity.MarkAsDeleted();
    }

    public async Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken)
    {
        if (!entities.Any()) {
            throw new InvalidOperationException("A lista não contém elementos.");
        }

        bool allEntitiesExist = _context.Set<T>().All(t => entities.Select(ent => ent.Id).Contains(t.Id));
        if (!allEntitiesExist) {
            throw new KeyNotFoundException($"Todas as entidades da lista devem existir no banco de dados.");
        }

        await _context.Set<T>()
            .Where(t => entities.Select(ent => ent.Id).Contains(t.Id))
            .ExecuteUpdateAsync(setPropCall => setPropCall.SetProperty(p => p.IsDeleted, true));
    }

    public async Task<T> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result ?? throw new NullReferenceException($"Entidade com Id {id} não encontrada.");
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public Task<bool> EntityExistsAsync(Guid entityId, CancellationToken cancellationToken)
    {
        return _context.Set<T>().AnyAsync(e => e.Id == entityId);
    }
}