using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IReceiptRepository : IBaseRepository<Receipt> 
{
    Task<List<Receipt>> GetReceiptsFromRaffleAsync(Guid raffleId, CancellationToken cancellationToken);
    Task<List<Receipt>> GetFilteredReceiptsAsync(ReceiptsGetFilteredQuery query, CancellationToken cancellationToken);
}
