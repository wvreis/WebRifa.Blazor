using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Raffle;

namespace WebRifa.Blazor.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class RafflesController(
    ILogger<RafflesController> logger,
    IRaffleService raffleService) : ControllerBase {

    [HttpGet]
    public async Task<ActionResult<List<RaffleDto>>> SearchRaffleAsync([FromQuery] RaffleSearchQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(SearchRaffleAsync)} executado");
        return await raffleService.SearchRaffleAsync(query, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<RaffleDto>> GetRaffleAsync([FromQuery] RaffleGetQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetRaffleAsync)} executado");
        return await raffleService.GetRaffleAsync(query, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<HashSet<int>>> GetFreeNumbersAsync([FromQuery] RaffleGetQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetFreeNumbersAsync)} executado");
        return await raffleService.GetFreeNumbersAsync(query, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddRaffleAsync([FromBody] RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(AddRaffleAsync)} executado");
        return await raffleService.AddRaffleAsync(raffleDto, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> BuyRaffleTicketsAsync([FromBody] BuyRaffleTicketsCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(BuyRaffleTicketsAsync)} executado");
        return await raffleService.BuyRaffleTicketsAsync(command, cancellationToken);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateRaffleAsync([FromBody] RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(UpdateRaffleAsync)} executado");
        await raffleService.UpdateRaffleAsync(raffleDto, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteRaffleAsync([FromBody] RaffleDto raffleDto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(DeleteRaffleAsync)} executado");
        await raffleService.DeleteRaffleAsync(raffleDto, cancellationToken);
        return NoContent();
    }
}
