using Identity.Api.Model.TenantModel;
using Identity.Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TenantCreateRequest request)
    {
        var tenant = Tenant.Create(request.Name, request.Description);
        return await Task.FromResult(StatusCode(StatusCodes.Status201Created));
    }
}