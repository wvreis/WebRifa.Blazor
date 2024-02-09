using WebRifa.Blazor.Core.Queries.Ticket;

namespace WebRifa.Blazor.Core.Interfaces.Repositories;

public interface ITicketRepository : IBaseRepository<Ticket> 
{
    Task<List<Ticket>> GetTicketsByRaffleIdAsync(GetTicketByRaffleIdQuery query, CancellationToken cancellationToken);
}
