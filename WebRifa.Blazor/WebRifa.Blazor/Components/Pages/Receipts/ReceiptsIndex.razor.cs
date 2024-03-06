using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Components.Common.PopUp;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.Components.Pages.Receipts;
public partial class ReceiptsIndex() {
    [Parameter]
    public int CurrentPage { get; set; }

    public PaginatedList<ReceiptDto>? Receipts { get; set; }
    public string SelectedReceiptId { get; set; } = string.Empty;
    public ReceiptsGetFilteredQuery GetFilteredQuery { get; set; } = new();
    PopUp? PopUp { get; set; }
    ReceiptGetAllQuery Query { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Query.CurrentPage = CurrentPage;
        Receipts = await receiptService.GetAllReceiptsPaginatedAsync(Query);
    }

    public async Task DeleteReceiptAsync(Guid receiptId)
    {
        bool confirmedDeletion = await JS.ShowConfirmationMessage("Deseja confirmar a exclusão?");
        if (!confirmedDeletion) {
            return;
        }

        ReceiptDeleteCommand command = new() { 
            ReceiptId = receiptId 
        };

        var result = await receiptService.DeleteReceiptAsync(command);
        if (result.IsSuccessStatusCode) {
            Receipts?.Items.Remove(Receipts.Items.FirstOrDefault(r => r.Id == command.ReceiptId)!);
        }
    }

    async Task ShowReceiptDetails(Guid receitpId)
    {
        SelectedReceiptId = receitpId.ToString();
        await PopUp!.ToggleVisibilityAsync();
    }

    string GetNumbersAsString(ReceiptDto receipt) =>
        string.Join(", ", receipt.TicketsNumbers);
}