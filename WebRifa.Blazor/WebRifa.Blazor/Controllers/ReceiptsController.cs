using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Commands.Receipt;
using WebRifa.Blazor.Core.Requests.Queries.Receipt;

namespace WebRifa.Blazor.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ReceiptsController(
    ILogger<ReceiptsController> logger, IReceiptService receiptService) : ControllerBase {

    private readonly ILogger<ReceiptsController> _logger = logger;
    private readonly IReceiptService _receiptService = receiptService;

    [HttpGet]
    public async Task<ActionResult<List<ReceiptDto>>> GetAllReceiptsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetAllReceiptsAsync)} executado.");
        return await _receiptService.GetAllReceiptsAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<ReceiptDto>> GetReceiptAsync([FromQuery] ReceiptGetQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetReceiptAsync)} executado.");
        return await _receiptService.GetReceiptAsync(query, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<List<ReceiptDto>>> GetFilteredReceiptsAsync([FromQuery] ReceiptsGetFilteredQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(GetFilteredReceiptsAsync)} executado.");
        return await _receiptService.GetFilteredReceiptsAsync(query, cancellationToken);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteReceiptAsync([FromQuery] ReceiptDeleteCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(DeleteReceiptAsync)} foi executado.");
        await _receiptService.DeleteReceiptAsync(command, cancellationToken);
        return NoContent();
    }
}
