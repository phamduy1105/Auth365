using SharedKernel.Core.Base;

namespace Identity.Domain.Aggregates;

public sealed class Client : AggregateRoot
{
    private Client(Guid tenantId,
        string clientId,
        string clientUri,
        bool requirePkce = true,
        bool requireConsent = false)
    {
        TenantId = tenantId;
        ClientId = clientId;
        ClientUri = clientUri;
        RequirePkce = requirePkce;
        RequireConsent = requireConsent;
    }

    public string ClientId { get; private set; }
    public string ClientUri { get; private set; }
    
    private readonly List<string> _clientSecrets = [];
    public IReadOnlyCollection<string> ClientSecrets => _clientSecrets.AsReadOnly();
    public bool RequirePkce { get; private set; }
    public bool RequireConsent { get; private set; }
    
    private readonly List<string> _redirectUris = [];
    public IReadOnlyCollection<string> RedirectUris => _redirectUris.AsReadOnly();
    
    private readonly List<string> _postLogoutRedirectUris = [];
    public IReadOnlyCollection<string> PostLogoutRedirectUris => _postLogoutRedirectUris.AsReadOnly();
    
    private readonly List<string> _allowedScopes = ["openid email profile offline_access"] ;
    public IReadOnlyCollection<string> AllowedScopes => _allowedScopes.AsReadOnly();

    public static Client Create(Guid tenantId,
        string clientId, 
        string clientUri,
        string clientSecret,
        string redirectUri,
        string postLogoutRedirectUri)
    {
        if (string.IsNullOrWhiteSpace(clientId)) 
            throw new ArgumentNullException(nameof(clientId));
        
        var client = new Client(tenantId,
            clientId,
            clientUri);
        client.AddClientSecret(clientSecret);
        client.AddRedirectUri(redirectUri);
        client.AddPostLogoutRedirectUri(postLogoutRedirectUri);

        return client;
    }

    public void AddRedirectUri(string uri)
    {
        if (!Uri.TryCreate(uri, UriKind.Absolute, out var outUri))
            throw new ArgumentException($"Invalid URI: {uri}");
        
        if (!string.IsNullOrEmpty(outUri.Fragment))
            throw new ArgumentException("Redirect URI must not contain fragment (#)");
        
        if (!_redirectUris.Contains(uri))
            _redirectUris.Add(uri);
    }

    public void AddPostLogoutRedirectUri(string uri)
    {
        if (!Uri.TryCreate(uri, UriKind.Absolute, out var outUri))
            throw new ArgumentException($"Invalid URI: {uri}");
        
        if (!string.IsNullOrEmpty(outUri.Fragment))
            throw new ArgumentException("Redirect URI must not contain fragment (#)");
        
        if (!_postLogoutRedirectUris.Contains(uri))
            _postLogoutRedirectUris.Add(uri);
    }

    public void AddClientSecret(string newSecretHash)
    {
        if (string.IsNullOrWhiteSpace(newSecretHash))
            throw new ArgumentException("Secret cannot be empty");
        
        _clientSecrets.Add(newSecretHash);
    }
}