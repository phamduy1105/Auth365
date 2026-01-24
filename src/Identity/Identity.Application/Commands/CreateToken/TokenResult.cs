namespace Identity.Application.Commands.CreateToken;

public sealed class TokenResult
{
    public string? IdToken { get; init; }
    public string AccessToken { get; init; } = string.Empty;
    public string? RefreshToken { get; init; }
    public string Scope { get; init; } = string.Empty;
    public long ExpiresIn { get; init; }
}