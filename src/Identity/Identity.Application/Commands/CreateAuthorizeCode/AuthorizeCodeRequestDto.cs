namespace Identity.Application.Commands.CreateAuthorizeCode;

public sealed record AuthorizeCodeRequestDto(string ClientId,
        string RedirectUri,
        string Scope,
        string? State,
        string? CodeChallenge,
        string? CodeChallengeMethod,
        string Nonce);