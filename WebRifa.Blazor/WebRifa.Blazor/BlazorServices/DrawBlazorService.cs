using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Draw;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.BlazorServices;

public class DrawBlazorService(HttpClient httpClient) : IDrawBlazorServices {
    const string baseURI = "api/draws";

    public async Task<PaginatedList<DrawDto>> GetAllDrawsAsync(DrawGetAllQuery query)
    {
        return await httpClient.GetFromJsonAsync<PaginatedList<DrawDto>>(
        $"{baseURI}/GetAllDraws" +
            $"{QueryStringBuilderHelper.GenerateQueryString(query)}") ?? new();
    }

    public async Task<HttpResponseMessage> CarryOutTheDrawAsync(CarryOutTheDrawCommand command)
    {
        return await httpClient.PostAsJsonAsync(
            $"{baseURI}/CarryOutTheDraw", 
            command);
    }
}
