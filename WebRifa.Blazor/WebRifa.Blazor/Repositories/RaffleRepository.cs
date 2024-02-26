using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;
using WebRifa.Blazor.Data;

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

    public async Task<List<Raffle>> GetDrawPendingRaffle(CancellationToken cancellationToken)
    {
        return await _context.Raffles
            .Where(raffle => raffle.CurrentState == Core.Enums.RaffleStates.DrawPending)
            .ToListAsync(cancellationToken);
    }

    public async Task<HashSet<int>> GetUsedNumbersAsync(Guid raffleId, CancellationToken cancellationToken)
    {        
        var result = await _context.Tickets
            .Where(ticket => !ticket.IsDeleted)
            .Where(ticket => ticket.RaffleId == raffleId)
            .Select(ticket => ticket.Number)
            .ToListAsync(cancellationToken);

        return result.ToHashSet();
    }

    public async Task<int> GetTotalNumberOfTicketsFromRaffleAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        var result = await _context.Raffles
            .Where(raffle => raffle.Id == raffleId)
            .Select(raffle => raffle.TotalNumberOfTickets)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}
