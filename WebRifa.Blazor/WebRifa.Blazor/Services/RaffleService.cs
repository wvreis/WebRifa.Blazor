using AutoMapper;
using WebRifa.Blazor.Core.Commands;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Queries.Raffle;
using WebRifa.Blazor.Core.Services;

namespace WebRifa.Blazor.Services;

public class RaffleService : IRaffleService 
{
    private readonly ILogger<RaffleService> _logger;
    private readonly IRaffleRepository _raffleRepository;
    private readonly IRaffleCoreService _raffleCoreService;
    private readonly IUnitOfWork _unitOfWork;

    private MapperConfiguration _mapperConfiguration;
    private IMapper? _mapper;

    public RaffleService(
        ILogger<RaffleService> logger,
        IUnitOfWork unitOfWork,
        IRaffleRepository raffleRepository,
        IRaffleCoreService raffleCoreService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _raffleRepository = raffleRepository;
        _raffleCoreService = raffleCoreService;

        _mapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Raffle, RaffleDto>().ReverseMap();
        });

        _mapper = _mapperConfiguration.CreateMapper();
    }

    public async Task<Guid> AddRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        try {
            Raffle raffle = _mapper?.Map<Raffle>(raffleDto) ?? throw new Exception("");

            await _raffleRepository.AddAsync(raffle, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("A Rifa {Id} foi adicionada.", raffle.Id);
            return raffle.Id;
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<bool> BuyRaffleTicketsAsync(BuyRaffleTicketsCommand command, CancellationToken cancellationToken)
    {
        try {
            await _raffleCoreService.BuyRaffleTicketsAsync(command, cancellationToken);

            _logger.LogInformation("Compra de Tickets Executada.");

            return await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<List<int>> GetFreeNumbersAsync(Guid raffleId, CancellationToken cancellationToken)
    {
        try {
            _logger.LogInformation("Get Números disponíveis executado para a rifa {raffleId}", raffleId);
            return await _raffleCoreService.GetFreeNumbersAsync(raffleId, cancellationToken);
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<RaffleDto> GetRaffleAsync(RaffleGetQuery query, CancellationToken cancellationToken)
    {
        try {
            var raffle = await _raffleRepository.GetAsync(query.Id, cancellationToken);

            _logger.LogInformation("Get de Rifa {Id} executado", query.Id);

            return _mapper?.Map<RaffleDto>(raffle) ?? throw new Exception();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<List<RaffleDto>> SearchRaffleAsync(RaffleSearchQuery query, CancellationToken cancellationToken)
    {
        try {
            var raffles = await _raffleRepository.SearchRaffleAsync(query, cancellationToken);

            _logger.LogInformation("Pesquisa de Rifas executada.");

            return _mapper?.Map<List<Raffle>, List<RaffleDto>>(raffles) ?? throw new Exception();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task UpdateRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        Raffle raffle = _mapper?.Map<Raffle>(raffleDto) ?? throw new Exception();

        _raffleRepository.Update(raffle);
        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation("A Rifa {Id} foi atualizada.", raffleDto.Id);
    }
}
