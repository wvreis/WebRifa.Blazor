using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Queries;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface IBuyerRepository : IBaseRepository<Buyer>
{
    public Task<List<Buyer>> SearchBuyersAsync(BuyerSearchQuery query, CancellationToken cancellationToken);
}
