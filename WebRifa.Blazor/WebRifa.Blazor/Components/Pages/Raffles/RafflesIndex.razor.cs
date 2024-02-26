using WebRifa.Blazor.Core.Dtos;

namespace WebRifa.Blazor.Components.Pages.Raffles;
public partial class RafflesIndex {
    public List<RaffleDto>? Raffles { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Raffles = await _raffleService.GetAllRafflesAsync();
    }

    public string GetEditLink(RaffleDto raffleDto) =>
        $"/raffle/{raffleDto.Id}";
}