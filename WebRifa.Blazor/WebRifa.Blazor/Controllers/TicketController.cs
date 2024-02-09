using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Queries.Ticket;

namespace WebRifa.Blazor.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TicketController : ControllerBase
{
    private readonly ILogger<TicketController> _logger;
    private readonly ITicketService _ticketService;

    public TicketController(
        ILogger<TicketController> logger, 
        ITicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<List<TicketDto>> GetTicketsByRaffleIdAsync([FromQuery] GetTicketByRaffleIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetTicketsByRaffleIdAsync)} executado.");
        return await _ticketService.GetTicketsByRaffleIdAsync(query, cancellationToken);
    }
}
