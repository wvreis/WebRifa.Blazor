using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.Commands;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Queries.Raffle;

namespace WebRifa.Blazor.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RaffleController : ControllerBase
{
    private readonly ILogger<RaffleController> _logger;
    private readonly IRaffleService _raffleService;

    public RaffleController(
        ILogger<RaffleController> logger,
        IRaffleService raffleService)
    {
        _logger = logger;
        _raffleService = raffleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RaffleDto>>> SearchRaffleAsync([FromQuery] RaffleSearchQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(SearchRaffleAsync)} executado");
        return await _raffleService.SearchRaffleAsync(query, cancellationToken);    
    }

    [HttpGet]
    public async Task<ActionResult<RaffleDto>> GetRaffleAsync([FromQuery] RaffleGetQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetRaffleAsync)} executado");
        return await _raffleService.GetRaffleAsync(query, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddRaffleAsync([FromBody] RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(AddRaffleAsync)} executado");
        return await _raffleService.AddRaffleAsync(raffleDto, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> BuyRaffleTicketsAsync([FromBody] BuyRaffleTicketsCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(BuyRaffleTicketsAsync)} executado");
        return await _raffleService.BuyRaffleTicketsAsync(command, cancellationToken);    
    }

    [HttpPut]
    public async Task<ActionResult> UpdateRaffleAsync([FromBody] RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(UpdateRaffleAsync)} executado");
        await _raffleService.UpdateRaffleAsync(raffleDto, cancellationToken);
        return Ok();
    }
}
