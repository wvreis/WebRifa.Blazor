using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Ticket;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository {
    private readonly IRaffleRepository _raffleRepository;

    public TicketRepository(ApplicationDbContext context, IRaffleRepository raffleRepository) : base(context)
    {
        _raffleRepository = raffleRepository;
    }

    public async Task<List<Ticket>> GetTicketsByRaffleIdAsync(
        GetTicketByRaffleIdQuery query,
        CancellationToken cancellationToken)
    {
        Expression<Func<Ticket, bool>> GetByRaffleId = ticket =>
            ticket.RaffleId == query.RaffleId;

        bool raffleExists = await _raffleRepository.EntityExistsAsync(query.RaffleId, cancellationToken);
        if (!raffleExists) {
            throw new KeyNotFoundException($"Rifa com Id {query.RaffleId} não existe");
        }

        //AutoInclude Navigation Disabled for BuyerTicketReceipt. Need to use explicit Include method.
        var result = await _context.Tickets
            .Include(x => x.BuyerTicketReceipt)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.BuyerTicketReceipt)
                .ThenInclude(x => x.Receipt)
            .Where(GetByRaffleId)
            .ToListAsync();

        return result;
    }
}
