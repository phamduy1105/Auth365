using Identity.Api.Model;
using Identity.Application.Commands.GetAuthorizeCode;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
        var result = await mediator.Send(new GetAuthorizeCodeCommand(requestDto));
        if (result.IsSuccess)
            //return Redirect(result.Value);
            return Ok(result.Value);
        return BadRequest(result.Message);
    }
}