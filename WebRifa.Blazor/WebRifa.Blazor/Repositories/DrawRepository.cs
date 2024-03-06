using Microsoft.EntityFrameworkCore;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Core.Requests.Queries.Draw;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class DrawRepository(
    ApplicationDbContext context) : BaseRepository<Draw>(context), IDrawRepository {

    public async Task<PaginatedList<Draw>> GetAllPaginatedAsync(DrawGetAllQuery query ,CancellationToken cancellationToken)
    {
        var draws = await context.Draws
            .IgnoreAutoIncludes()
            .AsSingleQuery()
            .Include(x => x.DrawnTicket)
                .ThenInclude(x => x.BuyerTicketReceipt)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Raffle)
            .Where(draw => !draw.IsDeleted)
            .Skip((query.CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        int totalCountItems = await context.Draws
            .IgnoreAutoIncludes()
            .AsSingleQuery()
            .Include(x => x.DrawnTicket)
                .ThenInclude(x => x.BuyerTicketReceipt)
                .ThenInclude(x => x.Buyer)
            .Include(x => x.Raffle)
            .Where(draw => !draw.IsDeleted)
            .CountAsync();

        int totalPages = (int)Math.Ceiling(
            Convert.ToDecimal(totalCountItems) /
            Convert.ToDecimal(PageSize));

        var paginatedList = new PaginatedList<Draw>(query.CurrentPage, totalPages, PageSize, draws);
        return paginatedList;
    }

    public async Task<bool> WasRaffleDrawDone(Guid raffleId, CancellationToken cancellationToken)
    {
        return await context.Draws
            .AnyAsync(
                draw => draw.RaffleId == raffleId,
                cancellationToken);
    }
}