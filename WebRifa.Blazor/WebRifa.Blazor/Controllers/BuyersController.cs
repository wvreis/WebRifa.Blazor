using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Queries.Buyer;

namespace WebRifa.Blazor.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class BuyersController(
    ILogger<BuyersController> logger,
    IBuyerService buyerService) : ControllerBase{
    private readonly ILogger<BuyersController> _logger = logger;
    private readonly IBuyerService _buyerService = buyerService;

    [HttpGet]
    public async Task<ActionResult<PaginatedList<BuyerDto>>> SearchBuyerAsync([FromQuery] BuyerSearchQuery query, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{nameof(SearchBuyerAsync)} executado.");
        return await _buyerService.SearchBuyerAsync(query, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<List<BuyerDto>>> GetAllBuyersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{nameof(SearchBuyerAsync)} executado.");
        return await _buyerService.GetAllBuyersAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<BuyerDto>> GetBuyerAsync([FromQuery] BuyerGetQuery query, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{nameof(GetBuyerAsync)} executado.");
        return await _buyerService.GetBuyerAsync(query, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddBuyerAsync([FromBody] BuyerDto buyerDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{nameof(AddBuyerAsync)} executado.");
        return await _buyerService.AddBuyerAsync(buyerDto, cancellationToken);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBuyerAsync([FromBody] BuyerDto buyer, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{nameof(UpdateBuyerAsync)} executado.");
        await _buyerService.UpdateBuyerAsync(buyer, cancellationToken);
        return Ok();
    }
}
