using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebRifa.Blazor.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ReceiptController(
    ) : ControllerBase{
}
