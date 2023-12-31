namespace WebRifa.Blazor.Core.Interfaces.Repositories;
public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken);
    void Dispose();

}
