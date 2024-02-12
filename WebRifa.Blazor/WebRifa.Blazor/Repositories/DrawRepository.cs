using WebRifa.Blazor.Core.Entities.DrawEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Repositories;
using WebRifa.Blazor.Data;
using WebRifa.Blazor.Services.UserServices;

namespace WebRifa.Blazor.Repositories;

public class DrawRepository(
    ApplicationDbContext context,
    ICustomUserIdProvider customUserIdProvider) : BaseRepository<Draw>(context, customUserIdProvider), IDrawRepository {
}
