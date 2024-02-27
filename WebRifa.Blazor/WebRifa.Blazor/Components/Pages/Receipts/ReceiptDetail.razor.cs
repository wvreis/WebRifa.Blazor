using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.Components.Pages.Receipts;
public partial class ReceiptDetail {
    [Parameter]
    public string Id { get; set; }

    public ReceiptGetQuery ReceiptGetQuery { get; set; } = new ReceiptGetQuery();
    public ReceiptDto? Receipt { get; set; }

    string ReceiptElementId => "receipttoexport";
    string ReceiptBuyerName => $"Recibo de {Receipt?.BuyerName ?? string.Empty}";

    protected override async Task OnInitializedAsync()
    {
        Guid.TryParse(Id, out Guid result);
        ReceiptGetQuery.ReceiptId = result;
        Receipt = await receiptService.GetReceiptAsync(ReceiptGetQuery);
    }

    async Task Export()
    {
        await JS.ExportToPDF(ReceiptElementId, ReceiptBuyerName);
    }

    bool IsLoading() => Receipt is null;

    string GetLinkToQRCode() =>
        $"{navigationManager.BaseUri}receipt/{Id}";
}