using AutoMapper;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.Services;

public class ReceiptService(
    ILogger<ReceiptService> logger,
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IReceiptService {

    private readonly ILogger<ReceiptService> _logger = logger;
    private readonly IReceiptRepository _receiptRepository = receiptRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<Receipt>> GetAllReceiptsAsync(CancellationToken cancellation)
    {
        try {
            var receipts = await _receiptRepository.GetAllAsync(cancellation);
            _logger.LogInformation("GetAll de Receipts foi executado.");
            return _mapper.Map<List<Receipt>>(receipts) ?? throw new NullReferenceException();
        }
        catch (Exception) {

            throw;
        }
    }

    public Task<List<Receipt>> GetReceiptsByBuyerAsync(BuyerGetQuery query, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public Task<List<Receipt>> GetReceiptsByRaffleAsync(RaffleGetQuery query, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }
}
