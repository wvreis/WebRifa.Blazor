using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Components.Pages.Receipts;
public partial class ReceiptsIndex() {
    public List<ReceiptDto>? Receipts { get; set; }
    public ReceiptsGetFilteredQuery GetFilteredQuery { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Receipts = await receiptService.GetFilteredReceiptsAsync(GetFilteredQuery);
    }



    string GetNumbersAsString(ReceiptDto receipt) =>
        string.Join(", ", receipt.TicketsNumbers);
}