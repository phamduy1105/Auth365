namespace Identity.Application.Commands.CreateToken;

public sealed record TokenRequestDto(string GrantType,
    string Code,
    string RedirectUri,
    string ClientId,
    string? ClientSecret,
    string? CodeVerifier);