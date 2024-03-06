using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.Components.Pages.Raffles;
public partial class RafflesIndex {
    [Parameter]
    public int CurrentPage { get; set; }

    public PaginatedList<RaffleDto>? Raffles { get; set; }
    RaffleSearchQuery Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Query.CurrentPage = CurrentPage;
        Raffles = await _raffleService.GetAllRafflesAsync(Query);
    }

    public string GetEditLink(RaffleDto raffleDto) =>
        $"/raffle/{raffleDto.Id}";
}