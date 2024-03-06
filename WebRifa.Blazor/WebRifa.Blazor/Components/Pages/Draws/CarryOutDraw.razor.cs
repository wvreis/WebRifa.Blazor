using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.Components.Pages.Draws;
public partial class CarryOutDraw {
    public List<RaffleDto>? Raffles { get; set; }
    public RaffleDto? SelectedRaffle { get; set; }
    public CarryOutTheDrawCommand CarryOutCommand { get; set; } = new();
    public int? DrawnNumber { get; set; }

    bool CarriengOut;

    protected async override Task OnInitializedAsync()
    {
        Raffles = await raffleService.GetDrawPendingRaffleAsync();
    }

    public async Task CarryOutTheDraw()
    {
        await StartSpinAnimationAndWait();

        CarryOutCommand.RaflleId = SelectedRaffle!.Id;
        var result = await drawService.CarryOutTheDrawAsync(CarryOutCommand);
        if (result.IsSuccessStatusCode) {
            var drawnNumber = await result.Content.ReadAsStringAsync();
            DrawnNumber = Convert.ToInt32(drawnNumber);
            CarriengOut = false;
            StateHasChanged();
        }
        else {
            await JS.ShowErrorMessage(result.ReasonPhrase!);
        }
    }

    void RaffleInputOnChange(ChangeEventArgs args)
    {
        if (string.IsNullOrEmpty(args.Value!.ToString()) || Raffles is null) {
            CleanScreen();
            return;
        }

        Guid.TryParse(args.Value.ToString(), out Guid raffleId);
        SelectedRaffle = Raffles.First(x => x.Id == raffleId);
        StateHasChanged();
    }

    void ReceiveSelectedRaffle(Object raffle)
    {
        SelectedRaffle = (RaffleDto)raffle;
        StateHasChanged();
    }

    async Task StartSpinAnimationAndWait()
    {
        CarriengOut = true;
        await Task.Delay(5000);
    }

    void CleanScreen()
    {
        SelectedRaffle = null;
    }

    bool ShowCarryOutButton() =>
        !CarriengOut && !DrawnNumber.HasValue && SelectedRaffle is not null;
}