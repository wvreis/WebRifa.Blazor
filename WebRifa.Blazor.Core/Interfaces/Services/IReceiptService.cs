using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Core.Interfaces.Services;
public interface IReceiptService {
    Task<List<ReceiptDto>> GetAllReceiptsAsync(CancellationToken cancellation);
    Task<ReceiptDto> GetReceiptAsync(ReceiptGetQuery query, CancellationToken cancellation);
    Task<List<ReceiptDto>> GetFilteredReceiptsAsync(ReceiptsGetFilteredQuery query, CancellationToken cancellation);
    Task<List<ReceiptDto>> GetReceiptsFromRaffleAsync(RaffleGetQuery query, CancellationToken cancellation);
    Task DeleteReceiptAsync(ReceiptDeleteCommand command, CancellationToken cancellation);
}
