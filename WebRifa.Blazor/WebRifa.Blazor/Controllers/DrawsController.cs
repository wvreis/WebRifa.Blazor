using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRifa.Blazor.Core.ApplicationModels;
using WebRifa.Blazor.Core.Dtos;
using WebRifa.Blazor.Core.Interfaces.Services;
using WebRifa.Blazor.Core.Requests.Commands;
using WebRifa.Blazor.Core.Requests.Queries.Draw;

namespace WebRifa.Blazor.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class DrawsController(
    ILogger<DrawsController> logger,
    IDrawService drawService) : ControllerBase {

    [HttpGet]
    public async Task<ActionResult<PaginatedList<DrawDto>>> GetAllDrawsAsync([FromQuery] DrawGetAllQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetAllDrawsAsync)} executado.");
        return await drawService.GetAllPaginatedAsync(query, cancellationToken);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> CarryOutTheDrawAsync([FromBody] CarryOutTheDrawCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(CarryOutTheDrawAsync)} executado");
        return await drawService.CarryOutTheDrawAsync(command, cancellationToken);
    }
}
