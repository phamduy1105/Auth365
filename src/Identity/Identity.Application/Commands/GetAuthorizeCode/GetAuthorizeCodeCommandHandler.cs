using Identity.Application.Interfaces;
using Identity.Domain.Abstractions;
using Identity.Domain.Aggregates;
using SharedKernel.Application.CQRS;
using SharedKernel.Application.Models;

namespace Identity.Application.Commands.GetAuthorizeCode;

public class GetAuthorizeCodeCommandHandler(IClientRepository clientRepository,
    ICurrentUser currentUser) : 
    ICommandHandler<GetAuthorizeCodeCommand, string>
{
    public async Task<Result<string>> Handle(GetAuthorizeCodeCommand request,
        CancellationToken cancellationToken)
    {
        var requestScope = request.AuthorizeCodeRequestDto.Scope.Split(' ').ToList();
        var finalGrantedScopes = new List<string>();
        
        var client =  await clientRepository.GetClientAsync(request.AuthorizeCodeRequestDto.ClientId);
        if (client == null)
            return Result<string>.Failure("Client not found",
                $"Please check info again, ClientId: {request.AuthorizeCodeRequestDto.ClientId}");
        
        var roles = currentUser.Roles;

        foreach (var scope in requestScope.Where(scope => client.AllowedScopes.Contains(scope)))
        {
            if (roles != null && (IsIdentityScope(scope) || roles.Contains(scope)))
            {
                finalGrantedScopes.Add(scope);
            }
        }
        var authCode = AuthorizationCode.Create(request.AuthorizeCodeRequestDto.ClientId,
            currentUser.UserId,
            request.AuthorizeCodeRequestDto.RedirectUri,
            finalGrantedScopes,
            request.AuthorizeCodeRequestDto.CodeChallenge,
            request.AuthorizeCodeRequestDto.CodeChallengeMethod,
            request.AuthorizeCodeRequestDto.Nonce
        );
        
        var redirectUrl = $"{request.AuthorizeCodeRequestDto.RedirectUri}" +
                          $"?code={authCode.Code}" +
                          $"&state={request.AuthorizeCodeRequestDto.State}" +
                          $"&scope={Uri.EscapeDataString(string.Join(" ", finalGrantedScopes))}";
        
        return Result<string>.Success(redirectUrl);
    }
    
    private static bool IsIdentityScope(string scope)
    {
        return new[] { "openid", "profile", "email", "phone", "offline_access" }.Contains(scope);
    }
}