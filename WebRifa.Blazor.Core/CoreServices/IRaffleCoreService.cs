using WebRifa.Blazor.Core.Requests.Commands;

namespace WebRifa.Blazor.Core.Services;
public interface IRaffleCoreService {
    Task BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken);
    Task<int> CarryOutTheDrawAsync(Guid raffleId, CancellationToken cancellationToken);
    Task<HashSet<int>> GetFreeNumbersAsync(Guid raffleId, CancellationToken cancellationToken);
    Task DeleteRaffleAsync(Guid raffleId, CancellationToken cancellationToken);
}