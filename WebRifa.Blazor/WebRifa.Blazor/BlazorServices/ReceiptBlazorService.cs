using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;
using WebRifa.Blazor.Helpers;

namespace WebRifa.Blazor.BlazorServices;

public class ReceiptBlazorService(
    HttpClient httpClient) : IReceiptBlazorService {

    const string baseURI = "api/receipts";

    public async Task<List<ReceiptDto>> GetAllReceiptsAsync()
    {
        return await httpClient
            .GetFromJsonAsync<List<ReceiptDto>>($"{baseURI}/GetAllReceipts") ?? new();
    }

    public async Task<List<ReceiptDto>> GetFilteredReceiptsAsync(ReceiptsGetFilteredQuery query)
    {
        return await httpClient.GetFromJsonAsync<List<ReceiptDto>>(
            $"{baseURI}/GetFilteredReceipts" +
            $"{QueryStringBuilderHelper.GenerateQueryString(query)}") ?? new();
    }

    public async Task<ReceiptDto> GetReceiptAsync(ReceiptGetQuery query)
    {
        return await httpClient.GetFromJsonAsync<ReceiptDto>(
            $"{baseURI}/GetReceipt" +
            $"{QueryStringBuilderHelper.GenerateQueryString(query)}") ?? new();
    }

    public async Task<HttpResponseMessage> DeleteReceiptAsync(ReceiptDeleteCommand command, CancellationToken cancellation)
    {
        return await httpClient.PostAsJsonAsync<ReceiptDeleteCommand>(
            $"{baseURI}/DeleteReceipt" +
            $"{QueryStringBuilderHelper.GenerateQueryString(command)}",
            null!);
    }
}
