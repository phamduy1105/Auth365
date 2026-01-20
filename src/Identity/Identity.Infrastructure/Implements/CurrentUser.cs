using Identity.Application.Interfaces;

namespace Identity.Infrastructure.Implements;

public class CurrentUser : ICurrentUser
{
    public string UserId { get; } = "55F91CA8-613D-4B74-BF8E-3B4417037262";
    public List<string> Roles { get; } = ["openid", "building.view"];
}