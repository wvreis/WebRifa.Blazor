using WebRifa.Blazor.Core.Requests.Queries.Buyer;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.Core.Interfaces.Services;
public interface IReceiptService {
    Task<List<Receipt>> GetAllReceiptsAsync(CancellationToken cancellation);
    Task<List<Receipt>> GetReceiptsByRaffleAsync(RaffleGetQuery query, CancellationToken cancellation);
    Task<List<Receipt>> GetReceiptsByBuyerAsync(BuyerGetQuery query, CancellationToken cancellation);
}
