using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Queries.Ticket;

namespace WebRifa.Blazor.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class TicketsController(
    ILogger<TicketsController> logger,
    ITicketService ticketService) : ControllerBase {
    private readonly ILogger<TicketsController> _logger = logger;
    private readonly ITicketService _ticketService = ticketService;

    [HttpGet]
    public async Task<List<TicketDto>> GetTicketsByRaffleIdAsync([FromQuery] GetTicketByRaffleIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetTicketsByRaffleIdAsync)} executado.");
        return await _ticketService.GetTicketsByRaffleIdAsync(query, cancellationToken);
    }

    [HttpGet]
    public async Task<List<TicketDto>> GetAllTicketsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetTicketsByRaffleIdAsync)} executado.");
        return await _ticketService.GetAlTicketsAsync(cancellationToken);
    }
}
