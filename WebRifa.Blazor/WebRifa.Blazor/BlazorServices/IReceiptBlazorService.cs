using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.BlazorServices;

public interface IReceiptBlazorService {
    Task<List<ReceiptDto>> GetAllReceiptsAsync();
    Task<List<ReceiptDto>> GetFilteredReceiptsAsync(ReceiptsGetFilteredQuery query);
    Task<ReceiptDto> GetReceiptAsync(ReceiptGetQuery query);
    Task<HttpResponseMessage> DeleteReceiptAsync(ReceiptDeleteCommand command);
    Task<ReceiptDto> GetPublicReceiptAsync(ReceiptGetQuery query);
    Task<PaginatedList<ReceiptDto>> GetAllReceiptsPaginatedAsync(ReceiptGetAllQuery query);
}
