using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;

namespace WebRifa.Blazor.BlazorServices;

public interface IDrawBlazorServices {
    Task<HttpResponseMessage> CarryOutTheDrawAsync(CarryOutTheDrawCommand command);
    Task<List<DrawDto>> GetAllDrawsAsync();
}
