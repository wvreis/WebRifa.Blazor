using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Components.Pages.Receipts;
public partial class ReceiptsIndex() {
    public List<ReceiptDto>? Receipts { get; set; }
    public ReceiptsGetFilteredQuery GetFilteredQuery { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Receipts = await receiptService.GetFilteredReceiptsAsync(GetFilteredQuery);
    }

    public async Task DeleteReceiptAsync(Guid receiptId)
    {
        ReceiptDeleteCommand command = new() { 
            ReceiptId = receiptId 
        };

        var result = await receiptService.DeleteReceiptAsync(command);
        if (result.IsSuccessStatusCode) {
            Receipts?.Remove(Receipts.FirstOrDefault(r => r.Id == command.ReceiptId)!);
        }
    }

    string GetNumbersAsString(ReceiptDto receipt) =>
        string.Join(", ", receipt.TicketsNumbers);
}