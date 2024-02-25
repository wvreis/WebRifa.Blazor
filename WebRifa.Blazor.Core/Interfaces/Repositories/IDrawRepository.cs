using WebRifa.Blazor.Core.Requests.Queries.Draw;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IDrawRepository : IBaseRepository<Draw> {
    Task<bool> WasRaffleDrawDone(Guid raffleId, CancellationToken cancellationToken);
}
