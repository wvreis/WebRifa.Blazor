using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.ApplicationModels;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IBuyerRepository : IBaseRepository<Buyer>
{
    public Task<PaginatedList<Buyer>> SearchBuyersAsync(BuyerSearchQuery query, CancellationToken cancellationToken);
}
