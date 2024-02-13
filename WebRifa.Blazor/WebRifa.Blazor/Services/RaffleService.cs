using AutoMapper;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;
using WebRifa.Blazor.Core.Services;

namespace WebRifa.Blazor.Services;

public class RaffleService : IRaffleService 
{
    private readonly ILogger<RaffleService> _logger;
    private readonly IRaffleRepository _raffleRepository;
    private readonly IRaffleCoreService _raffleCoreService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RaffleService(
        ILogger<RaffleService> logger,
        IUnitOfWork unitOfWork,
        IRaffleRepository raffleRepository,
        IRaffleCoreService raffleCoreService,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _raffleRepository = raffleRepository;
        _raffleCoreService = raffleCoreService;
        _mapper = mapper;
    }

    public async Task<Guid> AddRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        try {
            Raffle raffle = _mapper?.Map<Raffle>(raffleDto) ?? throw new ArgumentNullException();

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

    public async Task<int> CarryOutTheDrawAsync(CarryOutTheDrawCommand command, CancellationToken cancellationToken)
    {
        try {
            _logger.LogInformation("Sorteio executado para a rifa {RaflleId}", command.RaflleId);
            var drawnTicketNumber  = await _raffleCoreService.CarryOutTheDrawAsync(command.RaflleId, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return drawnTicketNumber;
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<HashSet<int>> GetFreeNumbersAsync(RaffleGetQuery query, CancellationToken cancellationToken)
    {
        try {
            _logger.LogInformation("Get Números disponíveis executado para a rifa {raffleId}", query.RaffleId);
            return await _raffleCoreService.GetFreeNumbersAsync(query.RaffleId, cancellationToken);
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<RaffleDto> GetRaffleAsync(RaffleGetQuery query, CancellationToken cancellationToken)
    {
        try {
            var raffle = await _raffleRepository.GetAsync(query.RaffleId, cancellationToken);

            _logger.LogInformation("Get de Rifa {Id} executado", query.RaffleId);

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
        try {
            Raffle raffle = _mapper?.Map<Raffle>(raffleDto) ?? throw new Exception();

            await _raffleRepository.UpdateAsync(raffle, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("A Rifa {Id} foi atualizada.", raffleDto.Id);
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task DeleteRaffleAsync(RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        try {
            Raffle raffle = _mapper?.Map<Raffle>(raffleDto) ?? throw new Exception();

            await _raffleCoreService.DeleteRaffleAsync(raffle.Id, cancellationToken);

            _logger.LogInformation("A Rifa {Id} foi deletada.", raffleDto.Id);
        }
        catch (Exception) {

            throw;
        }
    }
}
