using Microsoft.AspNetCore.Components;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Components.Pages.Receipts;
public partial class ReceiptDetail {
    [Parameter]
    public string Id { get; set; }

    public ReceiptGetQuery ReceiptGetQuery { get; set; } = new ReceiptGetQuery();
    public ReceiptDto? Receipt { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Guid.TryParse(Id, out Guid result);
        ReceiptGetQuery.ReceiptId = result;
        Receipt = await receiptService.GetReceiptAsync(ReceiptGetQuery);
    }

    bool IsLoading() => Receipt is null;

    string GetLinkToQRCode() =>
        $"{navigationManager.BaseUri}receipt/{Id}";
}