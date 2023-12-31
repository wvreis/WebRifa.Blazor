using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Common;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Core.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity {
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public async Task<T> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result ?? throw new NullReferenceException($"Entidade com Id {id} não encontrada.");
    }

    public async Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }
}