using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Queries;

namespace WebRifa.Blazor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyerController : ControllerBase{
    private readonly ILogger<BuyerController> _logger;
    private readonly IBuyerService _buyerService;

    public BuyerController(ILogger<BuyerController> logger, IBuyerService buyerService)
    {
        _logger = logger;
        _buyerService = buyerService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<List<BuyerDto>>> SearchBuyer([FromQuery] BuyerSearchQuery query, CancellationToken cancellationToken = default)
    {
        return await _buyerService.SearchBuyerAsync(query, cancellationToken);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<BuyerDto>> GetBuyerAsync([FromQuery] BuyerGetQuery query, CancellationToken cancellationToken = default)
    {
        return await _buyerService.GetBuyerAsync(query, cancellationToken);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<Guid>> AddBuyerAsync([FromBody] BuyerDto buyerDto, CancellationToken cancellationToken = default)
    {
        return await _buyerService.AddBuyerAsync(buyerDto, cancellationToken);
    }

}
