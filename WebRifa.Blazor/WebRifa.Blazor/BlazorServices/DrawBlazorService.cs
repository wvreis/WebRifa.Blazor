using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;

namespace WebRifa.Blazor.BlazorServices;

public class DrawBlazorService(HttpClient httpClient) : IDrawBlazorServices {
    const string baseURI = "api/draws";

    public async Task<List<DrawDto>> GetAllDrawsAsync()
    {
        return await httpClient.GetFromJsonAsync<List<DrawDto>>(
            $"{baseURI}/Draws/GetAllDrawsAsync") ?? new();
    }

    public async Task<HttpResponseMessage> CarryOutTheDrawAsync(CarryOutTheDrawCommand command)
    {
        return await httpClient.PostAsJsonAsync(
            $"{baseURI}/CarryOutTheDraw", 
            command);
    }
}
