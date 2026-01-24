namespace Identity.Application.Commands.CreateToken;

public class TokenRequestDto
{
    public string GrantType { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string RedirectUri { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string? ClientSecret { get; set; } = string.Empty;
    public string? CodeVerifier { get; set; } = string.Empty;
}