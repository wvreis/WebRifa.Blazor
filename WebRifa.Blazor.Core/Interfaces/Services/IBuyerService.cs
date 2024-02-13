﻿using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;

namespace WebRifa.Blazor.Core.Interfaces.Services;

public interface IBuyerService {
    Task<List<BuyerDto>> SearchBuyerAsync(BuyerSearchQuery query, CancellationToken cancellationToken);
    Task<BuyerDto> GetBuyerAsync(BuyerGetQuery query, CancellationToken cancellationToken);
    Task<Guid> AddBuyerAsync(BuyerDto buyerDto, CancellationToken cancellationToken);
    Task UpdateBuyerAsync(BuyerDto buyerDto, CancellationToken cancellationToken);
    Task DeleteBuyerAsync(BuyerDto buyerDto, CancellationToken cancellationToken);
}
