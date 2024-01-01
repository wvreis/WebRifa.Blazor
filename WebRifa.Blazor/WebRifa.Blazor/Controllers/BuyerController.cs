using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Queries.Buyer;

namespace WebRifa.Blazor.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BuyerController : ControllerBase{
    private readonly ILogger<BuyerController> _logger;
    private readonly IBuyerService _buyerService;

    public BuyerController(ILogger<BuyerController> logger, IBuyerService buyerService)
    {
        _logger = logger;
        _buyerService = buyerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BuyerDto>>> SearchBuyer([FromQuery] BuyerSearchQuery query, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{nameof(SearchBuyer)} executado.");
        return await _buyerService.SearchBuyerAsync(query, cancellationToken);
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
