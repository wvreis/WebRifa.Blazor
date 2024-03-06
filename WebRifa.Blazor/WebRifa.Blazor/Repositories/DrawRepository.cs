using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Draw;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class DrawRepository(
    ApplicationDbContext context) : BaseRepository<Draw>(context), IDrawRepository {

    public override async Task<List<Draw>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Draws
            .IgnoreAutoIncludes()
            .Include(x => x.DrawnTicket)
                .ThenInclude(x => x.BuyerTicketReceipt)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Raffle)
            .Where(draw => !draw.IsDeleted)
            .ToListAsync();
    }

    public async Task<bool> WasRaffleDrawDone(Guid raffleId, CancellationToken cancellationToken)
    {
        return await context.Draws
            .AnyAsync(
                draw => draw.RaffleId == raffleId,
                cancellationToken);
    }
}