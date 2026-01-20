using Identity.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("[controller]/v2.0")]
public class ConnectController : ControllerBase
{
    [HttpGet("authorize")]
    public async Task<IActionResult> Authorize([FromQuery] AuthorizeRequest request)
    {
        await Task.CompletedTask;
        return Ok(request);
    }
}