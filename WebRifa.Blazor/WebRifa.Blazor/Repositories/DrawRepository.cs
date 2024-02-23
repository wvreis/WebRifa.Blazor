using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Repositories;

public class DrawRepository(
    ApplicationDbContext context) : BaseRepository<Draw>(context), IDrawRepository {

    public override async Task<List<Draw>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Draws
            .IgnoreAutoIncludes()
            .Include(x => x.DrawnTicket)
                .ThenInclude(x => x.BuyerTicketReceipt)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Raffle)
            .Where(draw => !draw.IsDeleted)
            .ToListAsync();
    }
}
