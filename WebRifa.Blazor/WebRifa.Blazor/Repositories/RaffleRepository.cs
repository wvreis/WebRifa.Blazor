using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Queries.Raffle;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class RaffleRepository : BaseRepository<Raffle>, IRaffleRepository 
{
    public RaffleRepository(ApplicationDbContext context) : base(context)
    {}

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
