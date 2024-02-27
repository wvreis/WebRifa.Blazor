using Microsoft.AspNetCore.Components;
using Net.Codecrete.QrCodeGenerator;
using System.Security.Cryptography;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;
using WebRifa.Blazor.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace WebRifa.Blazor.Components.Pages.Receipts;
public partial class ReceiptDetail {
    [Parameter]
    public string Id { get; set; }

    public ReceiptGetQuery ReceiptGetQuery { get; set; } = new ReceiptGetQuery();
    public ReceiptDto? Receipt { get; set; }

    string ReceiptElementId => "receipttoexport";
    string ReceiptBuyerName => $"Recibo de {Receipt?.BuyerName ?? string.Empty}";
    string QrCodeBase64 { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Guid.TryParse(Id, out Guid result);
        ReceiptGetQuery.ReceiptId = result;
        Receipt = await receiptService.GetPublicReceiptAsync(ReceiptGetQuery);
        QrCodeBase64 = GetQrCodeBase64();
    }

    async Task Export()
    {
        await JS.ExportToPDF(ReceiptElementId, ReceiptBuyerName);
    }

    bool IsLoading() => 
        Receipt is null &&
        string.IsNullOrEmpty(QrCodeBase64);

    string GetLinkToQRCode() =>
        $"{navigationManager.BaseUri}receipt/{Id}";

    string GetQrCodeBase64()
    {
        var qr = QrCode.EncodeText(GetLinkToQRCode(), QrCode.Ecc.Medium);
        var bytes = qr.ToBmpBitmap(0,4);

        return Convert.ToBase64String(bytes);
    }
}