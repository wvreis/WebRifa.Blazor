using WebRifa.Blazor.Core.Commands;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Queries.Buyer;
using WebRifa.Blazor.Core.Queries.Raffle;

namespace WebRifa.Blazor.Core.Interfaces.Services;

public interface IRaffleService {
    Task<List<RaffleDto>> SearchRaffleAsync(RaffleSearchQuery query, CancellationToken cancellationToken);
    Task<RaffleDto> GetRaffleAsync(RaffleGetQuery query, CancellationToken cancellationToken);
    Task<Guid> AddRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken);
    Task UpdateRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken);
    Task<HashSet<int>> GetFreeNumbersAsync(RaffleGetQuery query, CancellationToken cancellationToken);
    Task<bool> BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken);
    Task<int> CarryOutTheDrawAsync(CarryOutTheDrawCommand command, CancellationToken cancellationToken);
}
