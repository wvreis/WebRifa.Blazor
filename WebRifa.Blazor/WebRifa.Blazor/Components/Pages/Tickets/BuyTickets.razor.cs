using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.Components.Pages.Tickets;
public partial class BuyTickets {
    public List<RaffleDto>? Raffles { get; set; }
    public List<BuyerDto>? Buyers { get; set; }
    public RaffleDto? SelectedRaffle { get; set; }
    public BuyerDto? SelectedBuyer { get; set; }
    public List<int> Numbers { get; set; } = new();
    public List<int> FreeNumbers { get; set; } = new();
    public BuyRaffleTicketsCommand buyCommand { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Raffles = await _raffleService.GetDrawPendingRaffleAsync();
        Buyers = await _buyerService.GetAllBuyersAsync();
    }

    async Task ConfirmBuyAsync()
    {
        buyCommand.Observations = string.Empty; //to-do: create field.

        var result = await _raffleService.BuyRaffleTicketsAsync(buyCommand);
        if (result.IsSuccessStatusCode) {
            string resultValue = await result.Content.ReadAsStringAsync();
            _navigationManager.NavigateTo($"receipt/{resultValue.RemoveQuotes()}");
        }
        else {
            await JS.ShowErrorMessage(result.ReasonPhrase ?? string.Empty);
        }
    }

    async Task RaffleInputOnChange()
    {
        if (Raffles is null) {
            CleanScreen();
            return;
        }

        buyCommand.RaffleId = SelectedRaffle!.Id;
        FreeNumbers = await _raffleService.GetFreeNumbersAsync(new() { RaffleId = SelectedRaffle.Id });
        Numbers = Enumerable.Range(1, SelectedRaffle.TotalNumberOfTickets).ToList();
        StateHasChanged();
    }

    void BuyerInputOnChenge()
    {
        if (Buyers is null) {
            CleanSelectedBuyer();
            return;
        }

        buyCommand.BuyerId = SelectedBuyer!.Id;
        StateHasChanged();
    }

    void CleanScreen()
    {
        Numbers = new();
        buyCommand.NumbersToBuy = new();
        SelectedRaffle = null;
        SelectedBuyer = null;
    }

    void CleanSelectedBuyer()
    {
        SelectedBuyer = null;
        buyCommand.NumbersToBuy = new();
    }

    void AddSelectedNumber(int number)
    {
        buyCommand.NumbersToBuy.Add(number);
    }

    void RemoveSelectedNumber(int number)
    {
        buyCommand.NumbersToBuy.Remove(number);
    }

    void AddOrRemoveNumber(int number)
    {
        if (!IsSelectedNumber(number) && IsFreeNumber(number)) {
            AddSelectedNumber(number);
        }
        else {
            RemoveSelectedNumber(number);
        }

        StateHasChanged();
    }

    async Task ReceiveSelectedRaffle(object raffleObj)
    {
        if (raffleObj is null) {
            CleanScreen();
            return; 
        }

        if (raffleObj is RaffleDto raffle) {
            SelectedRaffle = raffle;
            await RaffleInputOnChange();
        }
    }

    void ReceiveSelectedBuyer(object buyerObj)
    {
        if (buyerObj is BuyerDto buyer) {
            SelectedBuyer = buyer;
            BuyerInputOnChenge();
        }
    }

    bool IsFreeNumber(int number) =>
        FreeNumbers.Contains(number);

    bool IsSelectedNumber(int number) =>
        buyCommand.NumbersToBuy.Contains(number);

    string GetNumberClass(int number)
    {
        if (!IsFreeNumber(number)) {
            return "unavailable-number";
        }

        if (IsSelectedNumber(number)) {
            return "selected-number";
        }

        if (IsFreeNumber(number) && !IsSelectedNumber(number)) {
            return "available-number";
        }

        return string.Empty;
    }

    bool IsLoading() =>
        Raffles is null &&
        Buyers is null;
}