using System.Linq.Expressions;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class BuyerRepository(
    ApplicationDbContext context) : BaseRepository<Buyer>(context), IBuyerRepository {
    public async Task<PaginatedList<Buyer>> SearchBuyersAsync(BuyerSearchQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Buyer, bool>> Search = buyer =>
            string.IsNullOrEmpty(query.SearchTerm) ||
            buyer.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
            buyer.PhoneNumber.ToLower().Contains(query.SearchTerm.ToLower()) ||
            buyer.Email.ToLower().Contains(query.SearchTerm.ToLower());

        var result = await GetPaginatedEntitiesAsync(query.CurrentPage, context, cancellationToken, Search);

        return result;
    }
}
