using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;

namespace WebRifa.Blazor.Core.Interfaces.Services;
public interface IDrawService {
    Task<int> CarryOutTheDrawAsync(CarryOutTheDrawCommand command, CancellationToken cancellationToken);
    Task<List<DrawDto>> GetAllAsync(CancellationToken cancellationToken);
}
