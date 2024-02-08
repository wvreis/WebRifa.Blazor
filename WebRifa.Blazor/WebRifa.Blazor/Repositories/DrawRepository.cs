using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;

namespace WebRifa.Blazor.Repositories;

public class DrawRepository : BaseRepository<Draw>, IDrawRepository {
    public DrawRepository(ApplicationDbContext context) : base(context)
    {
    }
}
