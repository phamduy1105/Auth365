namespace Identity.Api.Model.ClientModel;

public record ClientCreateRequest(
    Guid TenantId,
    string ClientId,
    string ClientUri,
    string ClientSecret,
    string RedirectUri,
    string PostLogoutRedirectUri);
