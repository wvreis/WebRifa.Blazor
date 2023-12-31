﻿using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class UnitOfWork : IUnitOfWork {
    protected readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}