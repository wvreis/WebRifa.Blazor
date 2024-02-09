using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IRaffleRepository : IBaseRepository<Raffle> 
{
    Task<List<Raffle>> SearchRaffleAsync(RaffleSearchQuery query, CancellationToken cancellationToken);
}
