using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Repositories;

public class ReceiptRepository(
    ApplicationDbContext context,
    ICustomUserIdProvider customUserIdProvider) : BaseRepository<Receipt>(context, customUserIdProvider), IReceiptRepository {
}
