using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Dtos;
using AutoMapper;
using WebRifa.Blazor.Core.Entities;

namespace WebRifa.Blazor.Services;

public class BuyerService : IBuyerService {
    private readonly ILogger _logger;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BuyerService(
        ILogger logger,
        IBuyerRepository buyerRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _buyerRepository = buyerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddBuyer(BuyerDto buyerDto, CancellationToken cancellationToken)
    {
        try {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Buyer, BuyerDto>());
            var mapper = config.CreateMapper();

            Buyer buyer = mapper.Map<Buyer>(buyerDto);

            await _buyerRepository.AddAsync(buyer, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("O Comprador {Id} foi adicionado.", buyer.Id);
        }
        catch (Exception) {

            throw;
        }
    }
}
