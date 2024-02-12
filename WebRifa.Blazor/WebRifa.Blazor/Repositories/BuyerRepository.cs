using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Repositories;

public class BuyerRepository(
    ApplicationDbContext context, 
    ICustomUserIdProvider customUserIdProvider) : BaseRepository<Buyer>(context, customUserIdProvider), IBuyerRepository {
    public async Task<List<Buyer>> SearchBuyersAsync(BuyerSearchQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Buyer, bool>> Search = buyer =>
            string.IsNullOrEmpty(query.SearchTerm) ||
            buyer.Name.ToLower().Contains(query.SearchTerm.ToLower()) ||
            buyer.PhoneNumber.ToLower().Contains(query.SearchTerm.ToLower()) ||
            buyer.Email.ToLower().Contains(query.SearchTerm.ToLower());

        var result = await _context.Buyers
            .Where(Search)
            .ToListAsync(cancellationToken);

        return result;
    }
}
