using Identity.Application.Interfaces;

namespace Identity.Api.Contexts;

public sealed class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string UserId => httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value ?? "";
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}