using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.BlazorServices;

public class RaffleBlazorService : IRaffleBlazorService {
    private readonly HttpClient _httpClient;

    const string baseURI = "api/raffles";
    
    public RaffleBlazorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RaffleDto>> GetAllRafflesAsync(RaffleSearchQuery? raffleSearchQuery = null)
    {
        return await _httpClient.GetFromJsonAsync<List<RaffleDto>>(
            $"{baseURI}/SearchRaffle" +
            $"{QueryStringBuilderHelper.GenerateQueryString(raffleSearchQuery)}") ?? new();
    }

    public async Task<RaffleDto> GetRaffleAsync(RaffleGetQuery raffleGetQuery)
    {
        return await _httpClient.GetFromJsonAsync<RaffleDto>(
            $"{baseURI}/GetRaffle" +
            $"{QueryStringBuilderHelper.GenerateQueryString(raffleGetQuery)}") ?? new();
    }

    public async Task<List<int>> GetFreeNumbers(RaffleGetQuery raffleGetQuery)
    {
        return await _httpClient.GetFromJsonAsync<List<int>>(
            $"{baseURI}/GetFreeNumbers" +
            $"{QueryStringBuilderHelper.GenerateQueryString(raffleGetQuery)}") ?? new();
    }

    public async Task<HttpResponseMessage> AddRaffleAsync(RaffleDto raffleDto)
    {
        return await _httpClient.PostAsJsonAsync(
            $"{baseURI}/AddRaffle",
            raffleDto);
    }

    public async Task<HttpResponseMessage> UpdateRaffleAsync(RaffleDto raffleDto)
    {
        return await _httpClient.PutAsJsonAsync(
            $"{baseURI}/UpdateRaffle?Id={raffleDto.Id}",
            raffleDto);
    }

    public async Task<HttpResponseMessage> BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command)
    {
        return await _httpClient.PostAsJsonAsync(
            $"{baseURI}/BuyRaffleTickets", 
            command);
    }


}
