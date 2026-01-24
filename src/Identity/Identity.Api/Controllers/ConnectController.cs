using Identity.Api.Model;
using Identity.Application.Commands.CreateAuthorizeCode;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SharedKernel.Application.Models.Result;

namespace Identity.Api.Controllers;

[ApiController]
[Route("[controller]/v2.0")]
public class ConnectController(IMediator mediator) : ControllerBase
{
    [HttpGet("authorize")]
    public async Task<IActionResult> Authorize([FromQuery] AuthorizeRequest request)
    {
        //if (User?.Identity == null || !User.Identity.IsAuthenticated)
        //{
        //    var currentUrl = Request.GetEncodedUrl();
        //    return RedirectToAction("Login", "Account", new { returnUrl = currentUrl });
        //}
            
        var requestDto = new AuthorizeCodeRequestDto(request.ClientId,
            request.RedirectUri,
            request.Scope,
            request.State,
            request.CodeChallenge,
            request.CodeChallengeMethod,
            request.Nonce); 
        var result = await mediator.Send(new CreateAuthorizeCodeCommand(requestDto));
        switch (result)
        {
            case ValueApplicationResult<string> value:
                //return Redirect(value);
                return Ok(value);
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

    [HttpPost("token")]
    public async Task<IActionResult> Token([FromForm] TokenRequest request)
    {
        return Ok();
    }
}