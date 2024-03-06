using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Requests.Queries.Draw;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IDrawRepository : IBaseRepository<Draw> {
    Task<PaginatedList<Draw>> GetAllPaginatedAsync(DrawGetAllQuery query, CancellationToken cancellationToken);
    Task<bool> WasRaffleDrawDone(Guid raffleId, CancellationToken cancellationToken);
}
