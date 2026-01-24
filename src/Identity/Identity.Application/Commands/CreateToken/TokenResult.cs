using System.Text.Json.Serialization;

namespace Identity.Application.Commands.CreateToken;

public sealed class TokenResult
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IdToken { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RefreshToken { get; set; }
    public string Scope { get; set; } = string.Empty;
    public long ExpiresIn { get; set; }
}