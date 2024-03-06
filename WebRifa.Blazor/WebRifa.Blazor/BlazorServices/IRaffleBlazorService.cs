using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.BlazorServices;

public interface IRaffleBlazorService {
    Task<PaginatedList<RaffleDto>> GetAllRafflesAsync(RaffleSearchQuery? RaffleSearchQuery = null);
    Task<RaffleDto> GetRaffleAsync(RaffleGetQuery RaffleGetQuery);
    Task<HttpResponseMessage> AddRaffleAsync(RaffleDto RaffleDto);
    Task<HttpResponseMessage> UpdateRaffleAsync(RaffleDto RaffleDto);
    Task<List<int>> GetFreeNumbersAsync(RaffleGetQuery raffleGetQuery);
    Task<HttpResponseMessage> BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command);
    Task<List<RaffleDto>> GetDrawPendingRaffleAsync(RaffleSearchQuery? raffleSearchQuery = null);
}
