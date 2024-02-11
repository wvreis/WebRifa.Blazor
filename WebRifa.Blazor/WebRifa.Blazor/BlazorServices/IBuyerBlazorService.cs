using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;

namespace WebRifa.Blazor.BlazorServices;

public interface IBuyerBlazorService {
    Task<List<BuyerDto>> GetAllBuyersAsync(BuyerSearchQuery? buyerSearchQuery = null);
    Task<BuyerDto> GetBuyerAsync(BuyerGetQuery buyerGetQuery);
    Task<HttpResponseMessage> AddBuyerAsync(BuyerDto buyerDto);
    Task<HttpResponseMessage> UpdateBuyerAsync(BuyerDto buyerDto);
}
