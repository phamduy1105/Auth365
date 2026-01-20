using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Model;

public sealed class AuthorizeRequest
{
    [FromQuery(Name = "client_id")]
    public string ClientId { get; set; } = string.Empty;
    [FromQuery(Name = "redirect_uri")]
    public string RedirectUri { get; set; } = string.Empty;
    [FromQuery(Name = "scope")]
    public string Scope { get; set; } = string.Empty;
    [FromQuery(Name = "state")]
    public string State { get; set; } = string.Empty;
    [FromQuery(Name = "code_challenge")]
    public string CodeChallenge { get; set; } = string.Empty;
    [FromQuery(Name = "code_challenge_method")]
    public string CodeChallengeMethod { get; set; } = string.Empty;
}