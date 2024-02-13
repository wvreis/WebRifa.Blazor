using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Repositories;

public class ReceiptRepository(
    ApplicationDbContext context,
    ICustomUserIdProvider customUserIdProvider) : BaseRepository<Receipt>(context, customUserIdProvider), IReceiptRepository {

    public async Task<List<Receipt>> GetReceiptsFromRaffleAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        Expression<Func<Receipt, bool>> fromRaffleId = receipt =>
            receipt.BuyerTicketReceipt.Any(btr => btr.Ticket.RaffleId == raffleId);

        var result = await _context.Receipts
            .Where(fromRaffleId)
            .ToListAsync();

        return result;
    }
}
