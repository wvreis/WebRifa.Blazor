using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Repositories;

public class RaffleRepository(
    ApplicationDbContext context) : BaseRepository<Raffle>(context), IRaffleRepository 
{
    public async Task<List<Raffle>> SearchRaffleAsync(RaffleSearchQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Raffle, bool>> Search = raffle =>
            string.IsNullOrEmpty(query.SearchTerm) ||
            raffle.Description.ToLower().Contains(query.SearchTerm.ToLower()) ||
            raffle.Observations.ToLower().Contains(query.SearchTerm.ToLower());

        var result = await _context.Raffles
            .Where(Search)
            .ToListAsync(cancellationToken);

        return result;
    }
}
