using WebRifa.Blazor.Core.Requests.Queries.Buyer;

namespace WebRifa.Blazor.Core.CoreServices;
public interface IReceiptCoreService {
    Task GetReceiptsFromBuyer(BuyerGetQuery query, CancellationToken cancellation);
}
