using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Draw;

namespace WebRifa.Blazor.BlazorServices;

public interface IDrawBlazorServices {
    Task<HttpResponseMessage> CarryOutTheDrawAsync(CarryOutTheDrawCommand command);
    Task<PaginatedList<DrawDto>> GetAllDrawsAsync(DrawGetAllQuery query);
}
