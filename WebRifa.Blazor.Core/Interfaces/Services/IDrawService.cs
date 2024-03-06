using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Draw;

namespace WebRifa.Blazor.Core.Interfaces.Services;
public interface IDrawService {
    Task<int> CarryOutTheDrawAsync(CarryOutTheDrawCommand command, CancellationToken cancellationToken);
    Task<PaginatedList<DrawDto>> GetAllPaginatedAsync(DrawGetAllQuery query, CancellationToken cancellationToken);
}
