namespace Identity.Application.Commands.CreateClient;

public sealed record ClientCreatedRequestDto(Guid TenantId,
    string ClientId,
    string ClientUri,
    string ClientSecret,
    string RedirectUri,
    string PostLogoutRedirectUri);