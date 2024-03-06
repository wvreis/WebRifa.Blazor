using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IRaffleRepository : IBaseRepository<Raffle> 
{
    Task<PaginatedList<Raffle>> SearchRaffleAsync(RaffleSearchQuery query, CancellationToken cancellationToken);
    Task<HashSet<int>> GetUsedNumbersAsync(Guid raffleId, CancellationToken cancellationToken);
    Task<int> GetTotalNumberOfTicketsFromRaffleAsync(Guid raffleId, CancellationToken cancellationToken);
    Task<List<Raffle>> GetDrawPendingRaffle(CancellationToken cancellationToken);
}
