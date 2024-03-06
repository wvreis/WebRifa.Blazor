using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IReceiptRepository : IBaseRepository<Receipt> 
{
    Task<List<Receipt>> GetReceiptsFromRaffleAsync(Guid raffleId, CancellationToken cancellationToken);
    Task<List<Receipt>> GetFilteredReceiptsAsync(ReceiptsGetFilteredQuery query, CancellationToken cancellationToken);
    Task<Receipt> GetPublicAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedList<Receipt>> GetAllPaginatedAsync(ReceiptGetAllQuery query, CancellationToken cancellationToken);
}
