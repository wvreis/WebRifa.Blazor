using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Queries.Ticket;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository {
    public TicketRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Ticket>> GetTicketsByRaffleIdAsync(GetTicketByRaffleIdQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Ticket, bool>> GetByRaffleId = ticket =>
            ticket.RaffleId == query.RaffleId;

        var result = await _context.Tickets.Where(GetByRaffleId).ToListAsync();
        return result;
    }
}
