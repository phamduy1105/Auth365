using Identity.Api.Model.ClientModel;
using Identity.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientCreateRequest request)
    {
        var client = Client.Create(request.TenantId,
            request.ClientId,
            request.ClientUri,
            request.ClientSecret,
            request.RedirectUri,
            request.PostLogoutRedirectUri);
        
        return await Task.FromResult(StatusCode(StatusCodes.Status201Created));
    }
    
    
}