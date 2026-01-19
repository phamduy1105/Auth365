using SharedKernel.Core.Base;

namespace Identity.Domain.Aggregates;

public sealed class AuthorizationCode : AggregateRoot
{
    private AuthorizationCode(string code,
        string clientId,
        string subjectId,
        string redirectUri,
        List<string> grantedScopes,
        string codeChallenge,
        string codeChallengeMethod = "S256")
    {
        Code = code;
        ClientId = clientId;
        SubjectId = subjectId;
        RedirectUri = redirectUri;
        GrantedScopes = grantedScopes;
        CodeChallenge = codeChallenge;
        CodeChallengeMethod = codeChallengeMethod;
    }
    
    public string Code { get; init; }
    public string ClientId { get; init; }
    public string SubjectId { get; init; }
    public string RedirectUri { get; init; }
    
    public string CodeChallenge { get; init; }
    public string CodeChallengeMethod { get; init; }

    public List<string> GrantedScopes { get; init; }

    public static AuthorizationCode Create(string clientId,
        string subjectId,
        string redirectUri,
        List<string> finalScopes,
        string codeChallenge)
    {
        var authorizationCode = new AuthorizationCode(Guid.NewGuid().ToString("N"),
            clientId,
            subjectId,
            redirectUri,
            finalScopes,
            codeChallenge);
        
        return authorizationCode;
    }
}