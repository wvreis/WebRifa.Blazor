using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class BuyerRepository : BaseRepository<Buyer>, IBuyerRepository {
    public BuyerRepository(ApplicationDbContext context) : base(context)
    {
    }
}
