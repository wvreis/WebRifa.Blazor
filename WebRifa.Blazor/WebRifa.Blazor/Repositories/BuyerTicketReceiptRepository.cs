using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Repositories;

public class BuyerTicketReceiptRepository(
    ApplicationDbContext context) : BaseRepository<BuyerTicketReceipt>(context), IBuyerTicketReceiptRepository {
}
