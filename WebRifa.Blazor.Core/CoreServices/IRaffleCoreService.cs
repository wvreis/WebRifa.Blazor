using WebRifa.Blazor.Core.Commands;

namespace WebRifa.Blazor.Core.Services;
public interface IRaffleCoreService {
    Task BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken);
    Task<List<int>> GetFreeNumbersAsync(Guid raffleId, CancellationToken cancellationToken);

}