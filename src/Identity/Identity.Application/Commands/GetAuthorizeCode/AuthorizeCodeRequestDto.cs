namespace Identity.Application.Commands.GetAuthorizeCode;

public record AuthorizeCodeRequestDto(string ClientId,
        string RedirectUri,
        string Scope,
        string State,
        string CodeChallenge,
        string CodeChallengeMethod);