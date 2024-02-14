using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.BlazorServices;

public interface IRaffleBlazorService {
    Task<List<RaffleDto>> GetAllRafflesAsync(RaffleSearchQuery? RaffleSearchQuery = null);
    Task<RaffleDto> GetRaffleAsync(RaffleGetQuery RaffleGetQuery);
    Task<HttpResponseMessage> AddRaffleAsync(RaffleDto RaffleDto);
    Task<HttpResponseMessage> UpdateRaffleAsync(RaffleDto RaffleDto);
    Task<List<int>> GetFreeNumbers(RaffleGetQuery raffleGetQuery);
    Task<HttpResponseMessage> BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command);
}
