using AutoMapper;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.CoreServices;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Entities.ReceiptEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Services;

public class ReceiptService(
    ILogger<ReceiptService> logger,
    IReceiptRepository receiptRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IReceiptCoreService receiptCoreService) : IReceiptService {

    private readonly ILogger<ReceiptService> _logger = logger;
    private readonly IReceiptRepository _receiptRepository = receiptRepository;
    private readonly IReceiptCoreService _receiptCoreService = receiptCoreService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<List<ReceiptDto>> GetAllReceiptsAsync(CancellationToken cancellation)
    {
        try {
            var receipts = await _receiptRepository.GetAllAsync(cancellation);
            _logger.LogInformation($"{nameof(GetAllReceiptsAsync)} foi executado.");
            return _mapper.Map<List<ReceiptDto>>(receipts) ?? throw new NullReferenceException();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<PaginatedList<ReceiptDto>> GetAllPaginatedAsync(ReceiptGetAllQuery query, CancellationToken cancellation)
    {
        try {
            var receipts = await _receiptRepository.GetAllPaginatedAsync(query, cancellation);
            _logger.LogInformation($"{nameof(GetAllPaginatedAsync)} foi executado.");
            return _mapper.Map<PaginatedList<Receipt>, PaginatedList<ReceiptDto>>(receipts) ?? throw new NullReferenceException();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<ReceiptDto> GetReceiptAsync(ReceiptGetQuery query, CancellationToken cancellation)
    {
        try {
            var receipt = await _receiptRepository.GetAsync(query.ReceiptId, cancellation);
            _logger.LogInformation($"{nameof(GetReceiptAsync)} foi executado.");
            return _mapper.Map<ReceiptDto>(receipt) ?? throw new NullReferenceException();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<ReceiptDto> GetPublicReceiptAsync(ReceiptGetQuery query, CancellationToken cancellation)
    {
        try {
            var receipt = await _receiptRepository.GetPublicAsync(query.ReceiptId, cancellation);
            _logger.LogInformation($"{nameof(GetPublicReceiptAsync)} foi executado.");
            return _mapper.Map<ReceiptDto>(receipt) ?? throw new NullReferenceException();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<List<ReceiptDto>> GetFilteredReceiptsAsync(ReceiptsGetFilteredQuery query, CancellationToken cancellation)
    {
        try {
            var receipts = await _receiptRepository.GetFilteredReceiptsAsync(query, cancellation);
            _logger.LogInformation($"{nameof(GetFilteredReceiptsAsync)}foi executado.");
            return _mapper.Map<List<ReceiptDto>>(receipts);
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<List<ReceiptDto>> GetReceiptsFromRaffleAsync(RaffleGetQuery query, CancellationToken cancellation)
    {
        try {
            var receipts = await _receiptRepository.GetReceiptsFromRaffleAsync(query.RaffleId, cancellation);
            _logger.LogInformation($"{nameof(GetReceiptsFromRaffleAsync)} executado.");
            return _mapper.Map<List<ReceiptDto>>(receipts);
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task DeleteReceiptAsync(ReceiptDeleteCommand command, CancellationToken cancellation)
    {
        try {
            _logger.LogInformation($"{nameof(DeleteReceiptAsync)} executado");
            await _receiptCoreService.DeleteReceiptAsync(command, cancellation);
            await _unitOfWork.CommitAsync(cancellation);
        }
        catch (Exception) {

            throw;
        }
    }

}
