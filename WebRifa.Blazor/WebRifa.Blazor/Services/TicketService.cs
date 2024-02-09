using AutoMapper;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Entities;
using WebRifa.Blazor.Core.Entities.TicketEntities;
using WebRifa.Blazor.Core.Interfaces.Repositories;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Queries.Ticket;
using WebRifa.Blazor.Repositories;

namespace WebRifa.Blazor.Services;

public class TicketService : ITicketService 
{
    private readonly ILogger<TicketService> _logger;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    private MapperConfiguration _mapperConfiguration;
    private IMapper? _mapper;

    public TicketService(
        ILogger<TicketService> logger,
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;

        _mapperConfiguration = new MapperConfiguration(cfg => {
            cfg.CreateMap<Ticket, TicketDto>().ReverseMap();
            cfg.CreateMap<BuyerTicketReceipt, BuyerTicketReceiptDto>().ReverseMap();
        });

        _mapper = _mapperConfiguration.CreateMapper();
    }

    public async Task<Guid> AddTicketAsync(TicketDto ticketDto, CancellationToken cancellationToken)
    {
        try {
            Ticket ticket = _mapper?.Map<Ticket>(ticketDto) ?? throw new ArgumentNullException();

            await _ticketRepository.AddAsync(ticket, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("O Ticket {Id} foi adicionada.", ticket.Id);
            return ticket.Id;
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<TicketDto> GetTicketAsync(TicketGetQuery query, CancellationToken cancellationToken)
    {
        try {
            var ticket = await _ticketRepository.GetAsync(query.TicketId, cancellationToken);
            _logger.LogInformation("Get de Ticket {Id} executado", query.TicketId);

            return _mapper?.Map<TicketDto>(ticket) ?? throw new Exception();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task<List<TicketDto>> GetTicketsByRaffleIdAsync(GetTicketByRaffleIdQuery query, CancellationToken cancellationToken)
    {
        try {
            var tickets = await _ticketRepository.GetTicketsByRaffleIdAsync(query, cancellationToken);
            _logger.LogInformation("Get de Tickets por Id de Rifa {Id} executado", query.RaffleId);

            return _mapper?.Map<List<Ticket>, List<TicketDto>>(tickets) ?? throw new Exception();
        }
        catch (Exception) {

            throw;
        }
    }

    public async Task UpdateTicketAsync(TicketDto ticketDto, CancellationToken cancellationToken)
    {
        try {
            Ticket ticket = _mapper?.Map<Ticket>(ticketDto) ?? throw new Exception();

            _ticketRepository.Update(ticket);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("A Rifa {Id} foi atualizada.", ticketDto.Id);
        }
        catch (Exception) {

            throw;
        }
    }
}
