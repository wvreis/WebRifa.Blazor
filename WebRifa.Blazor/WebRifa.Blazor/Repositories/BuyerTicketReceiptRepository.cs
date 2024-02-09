using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class BuyerTicketReceiptRepository : BaseRepository<BuyerTicketReceipt>, IBuyerTicketReceiptRepository {
    public BuyerTicketReceiptRepository(ApplicationDbContext context) : base(context)
    {
    }
}
