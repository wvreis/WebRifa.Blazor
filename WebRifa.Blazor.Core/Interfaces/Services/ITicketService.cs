using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Requests.Queries.Ticket;

namespace WebRifa.Blazor.Core.Interfaces.Services;

public interface ITicketService {
    Task<TicketDto> GetTicketAsync(TicketGetQuery query, CancellationToken cancellationToken);
    Task<List<TicketDto>> GetTicketsByRaffleIdAsync(GetTicketByRaffleIdQuery query, CancellationToken cancellationToken);
    Task<Guid> AddTicketAsync(TicketDto ticketDto, CancellationToken cancellationToken);
    Task UpdateTicketAsync(TicketDto ticketDto, CancellationToken cancellationToken);
}
