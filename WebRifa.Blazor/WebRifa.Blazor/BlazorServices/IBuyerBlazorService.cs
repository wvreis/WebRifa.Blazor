using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;

namespace WebRifa.Blazor.BlazorServices;

public interface IBuyerBlazorService {
    Task<List<BuyerDto>> GetAllBuyersAsync();
    Task<PaginatedList<BuyerDto>> GetSearchBuyersAsync(BuyerSearchQuery? buyerSearchQuery = null);
    Task<BuyerDto> GetBuyerAsync(BuyerGetQuery buyerGetQuery);
    Task<HttpResponseMessage> AddBuyerAsync(BuyerDto buyerDto);
    Task<HttpResponseMessage> UpdateBuyerAsync(BuyerDto buyerDto);
}
