using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.BlazorServices;

public class BuyerBlazorService(HttpClient httpClient) : IBuyerBlazorService {
    private readonly HttpClient _httpClient = httpClient;
    const string baseURI = "api/buyers";

    public async Task<PaginatedList<BuyerDto>> GetSearchBuyersAsync(BuyerSearchQuery? buyerSearchQuery = null)
    {
        return await _httpClient.GetFromJsonAsync<PaginatedList<BuyerDto>>(
            $"{baseURI}/SearchBuyer" +
            $"{QueryStringBuilderHelper.GenerateQueryString(buyerSearchQuery)}") ?? new();
    }

    public async Task<List<BuyerDto>> GetAllBuyersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BuyerDto>>(
            $"{baseURI}/GetAllBuyers") ?? new();
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
