using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository {
    public ReceiptRepository(ApplicationDbContext context) : base(context)
    {
    }
}
