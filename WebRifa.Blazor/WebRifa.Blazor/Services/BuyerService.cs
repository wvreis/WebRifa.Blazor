using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Dtos;
using AutoMapper;
using WebRifa.Blazor.Core.Queries.Buyer;

namespace WebRifa.Blazor.Services;

public class BuyerService : IBuyerService
{
    private readonly ILogger<BuyerService> _logger;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IUnitOfWork _unitOfWork;

    private MapperConfiguration _mapperConfiguration;
    private IMapper? _mapper;

    public BuyerService(
        ILogger<BuyerService> logger,
        IBuyerRepository buyerRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _buyerRepository = buyerRepository;
        _unitOfWork = unitOfWork;

        _mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Buyer, BuyerDto>().ReverseMap();
        });

        _mapper = _mapperConfiguration.CreateMapper();
    }

    public async Task<List<BuyerDto>> SearchBuyerAsync(BuyerSearchQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var buyers = await _buyerRepository.SearchBuyersAsync(query, cancellationToken);

            _logger.LogInformation("Pesquisa de Compradores executada.");

            return _mapper?.Map<List<Buyer>, List<BuyerDto>>(buyers) ?? throw new Exception();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<BuyerDto> GetBuyerAsync(BuyerGetQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var buyer = await _buyerRepository.GetAsync(query.BuyerId, cancellationToken);

            _logger.LogInformation("Get de Comprador {BuyerId} executado.", query.BuyerId);

            return _mapper?.Map<BuyerDto>(buyer) ?? throw new Exception();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Guid> AddBuyerAsync(BuyerDto buyerDto, CancellationToken cancellationToken)
    {
        try
        {
            Buyer buyer = _mapper?.Map<Buyer>(buyerDto) ?? throw new Exception();

            await _buyerRepository.AddAsync(buyer, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("O Comprador {Id} foi adicionado.", buyer.Id);

            return buyer.Id;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task UpdateBuyerAsync(BuyerDto buyerDto, CancellationToken cancellationToken)
    {
        try
        {
            Buyer buyer = _mapper?.Map<Buyer>(buyerDto) ?? throw new Exception();

            _buyerRepository.Update(buyer);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("O Comprador {Id} foi atualizado.", buyer.Id);
        }
        catch (Exception)
        {

            throw;
        }
    }

}