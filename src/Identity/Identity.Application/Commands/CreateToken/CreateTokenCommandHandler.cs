using Identity.Application.Interfaces;
using SharedKernel.Application.CQRS;
using SharedKernel.Application.Models.Result;

namespace Identity.Application.Commands.CreateToken;

public sealed class CreateTokenCommandHandler(IAuthorizationCodeStore authorizationCodeStore) :
    ICommandHandler<CreateTokenCommand, TokenResult>
{
    public async Task<ApplicationResult> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var systemScopes = new HashSet<string>(["openid", "profile", "email", "phone", "address"],
            StringComparer.OrdinalIgnoreCase);
        
        var tokenResult = new TokenResult();
        
        var authorizationRequest =
            await authorizationCodeStore.GetCodeAsync(request.TokenRequestDto.Code);
        
        if (authorizationRequest is null)
            return new ErrorApplicationResult("Authorization Code not found");
        
        var isIdToken = authorizationRequest.GrantedScopes.Any(x => x is "openid");
        
        if (!isIdToken)
            tokenResult.IdToken = null;
        else
        {
            var idTokenScope = authorizationRequest
                .GrantedScopes
                .Where(x => systemScopes.Contains(x));
            tokenResult.IdToken = string.Join(" ", idTokenScope.Select(x => x.ToString()));
        }

        if (authorizationRequest.CodeChallenge is not null &&
            request.TokenRequestDto.CodeVerifier != authorizationRequest.CodeChallenge)
        {
            return new ErrorApplicationResult("Code verifier not match");
        }

        tokenResult.AccessToken = "eyBhdfieVBms...";
        var accessTokenScope = authorizationRequest
            .GrantedScopes
            .Where(x => !systemScopes.Contains(x));
        tokenResult.Scope = string.Join(" ", accessTokenScope.Select(x => x.ToString()));
        tokenResult.ExpiresIn = DateTime.UtcNow.AddHours(8).Ticks;

        return new ValueApplicationResult<TokenResult>(tokenResult);
    }
}