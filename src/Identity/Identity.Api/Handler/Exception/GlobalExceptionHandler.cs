using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Handler.Exception;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        System.Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
        };
        var traceId = httpContext.TraceIdentifier;
        problemDetails.Extensions["errors"] = exception.Message;
        problemDetails.Extensions["traceId"] = traceId;
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}