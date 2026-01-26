using Identity.Api.Model.ClientModel;
using Identity.Application.Commands.CreateClient;
using Identity.Application.Commands.CreateClientAddUri;
using Identity.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application.Models.Result;

namespace Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientCreateRequest request)
    {
        var clientRequest = new ClientCreatedRequestDto(request.TenantId,
            request.ClientId,
            request.ClientUri,
            request.ClientSecret,
            request.RedirectUri,
            request.PostLogoutRedirectUri);

        var result = await mediator.Send(new CreateClientCommand(clientRequest));

        switch (result)
        {
            case ValueApplicationResult<int>  value:
                return Ok(value.Value);
            case ErrorApplicationResult error:
            {
                var problemDetails = new ProblemDetails()
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                };
                var traceId = HttpContext.TraceIdentifier;
                problemDetails.Extensions["errors"] = error.ErrorMessage;
                problemDetails.Extensions["traceId"] = traceId;

                return BadRequest(problemDetails);
            }
            default:
                return BadRequest();
        }
    }

    [HttpPost("{id}/redirect-uris")]
    public async Task<IActionResult> AddRedirectUris(Guid id, [FromBody] ClientRedirectUriAdd clientRedirectUriAdd)
    {
        var result = 
            await mediator.Send(new CreateClientAddRedirectUriCommand(id,
            clientRedirectUriAdd.Value));
        
        switch (result)
        {
            case ValueApplicationResult<int>  value:
                return Ok(value.Value);
            case ErrorApplicationResult error:
            {
                var problemDetails = new ProblemDetails()
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                };
                var traceId = HttpContext.TraceIdentifier;
                problemDetails.Extensions["errors"] = error.ErrorMessage;
                problemDetails.Extensions["traceId"] = traceId;

                return BadRequest(problemDetails);
            }
            default:
                return BadRequest();
        }
    }
}