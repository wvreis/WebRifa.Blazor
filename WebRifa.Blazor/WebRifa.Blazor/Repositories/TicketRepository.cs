using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository {
    public TicketRepository(ApplicationDbContext context) : base(context)
    {
    }

    
}
