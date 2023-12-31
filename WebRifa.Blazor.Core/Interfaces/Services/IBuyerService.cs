using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Queries;

namespace WebRifa.Blazor.Core.Interfaces.Services;

public interface IBuyerService {
    Task<List<BuyerDto>> SearchBuyerAsync(BuyerSearchQuery query, CancellationToken cancellationToken);
    Task<BuyerDto> GetBuyerAsync(BuyerGetQuery query, CancellationToken cancellationToken);
    Task<Guid> AddBuyerAsync(BuyerDto buyerDto, CancellationToken cancellationToken);
}
