using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;

namespace WebRifa.Blazor.Components.Pages.Draws;
public partial class CarryOutDraw {
    public List<RaffleDto>? Raffles { get; set; }
    public RaffleDto? SelectedRaffle { get; set; }
    public CarryOutTheDrawCommand carryOutCommand { get; set; } = new();
    public int? DrawnNumber { get; set; }                                   

    protected async override Task OnInitializedAsync()
    {
        Raffles = await raffleService.GetAllRafflesAsync();
    }

    public async Task CarryOutTheDraw()
    {
        carryOutCommand.RaflleId = SelectedRaffle!.Id;
        var result = await drawService.CarryOutTheDrawAsync(carryOutCommand);
        if (result.IsSuccessStatusCode) {
            var drawnNumber = await result.Content.ReadAsStringAsync();
            DrawnNumber = Convert.ToInt32(drawnNumber);
            StateHasChanged();
        }
        else {
            //To-do: create SWAL to errors.
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

    void CleanScreen()
    {
        SelectedRaffle = null;
    }
}