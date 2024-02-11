using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.BlazorServices;

public class BuyerBlazorService : IBuyerBlazorService {
    private readonly HttpClient _httpClient;

    const string baseURI = "api/buyer";

    public BuyerBlazorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BuyerDto>> GetAllBuyersAsync(BuyerSearchQuery? buyerSearchQuery = null)
    {
        return await _httpClient.GetFromJsonAsync<List<BuyerDto>>(
            $"{baseURI}/SearchBuyer" +
            $"{QueryStringBuilderHelper.GenerateQueryString(buyerSearchQuery)}") ?? new();
    }

    public async Task<BuyerDto> GetBuyerAsync(BuyerGetQuery buyerGetQuery)
    {
        return await _httpClient.GetFromJsonAsync<BuyerDto>(
            $"{baseURI}/GetBuyer" +
            $"{QueryStringBuilderHelper.GenerateQueryString(buyerGetQuery)}") ?? new();
    }

    public async Task<HttpResponseMessage> AddBuyerAsync(BuyerDto buyerDto)
    {
        return await _httpClient.PostAsJsonAsync(
            $"{baseURI}/AddBuyer",
            buyerDto);
    }

    public async Task<HttpResponseMessage> UpdateBuyerAsync(BuyerDto buyerDto)
    {
        return await _httpClient.PutAsJsonAsync(
            $"{baseURI}/UpdateBuyer?Id={buyerDto.Id}", 
            buyerDto);
    }
}
