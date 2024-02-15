using WebRifa.Blazor.Core.Dtos;

namespace WebRifa.Blazor.BlazorServices;

public interface IReceiptBlazorService {
    Task<List<ReceiptDto>> GetAllReceiptsAsync();
    Task<ReceiptDto> GetReceiptAsync();
}
