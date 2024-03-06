using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.Core.Interfaces.Services;

public interface IRaffleService {
    Task<PaginatedList<RaffleDto>> SearchRaffleAsync(RaffleSearchQuery query, CancellationToken cancellationToken);
    Task<RaffleDto> GetRaffleAsync(RaffleGetQuery query, CancellationToken cancellationToken);
    Task<Guid> AddRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken);
    Task UpdateRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken);
    Task<HashSet<int>> GetFreeNumbersAsync(RaffleGetQuery query, CancellationToken cancellationToken);
    Task<Guid> BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken);
    Task DeleteRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken);
    Task<List<RaffleDto>> GetDrawPendingRaffleAsync(CancellationToken cancellationToken);
}
