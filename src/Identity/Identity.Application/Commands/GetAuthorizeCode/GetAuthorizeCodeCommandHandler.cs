using Identity.Application.Interfaces;
using Identity.Domain.Abstractions;
using Identity.Domain.Aggregates;
using SharedKernel.Application.CQRS;
using SharedKernel.Application.Models.Result;

namespace Identity.Application.Commands.GetAuthorizeCode;

public class GetAuthorizeCodeCommandHandler(IClientRepository clientRepository,
    IUserRepository userRepository,
    ICurrentUser currentUser) : 
    ICommandHandler<GetAuthorizeCodeCommand, string>
{
    public async Task<ApplicationResult> Handle(GetAuthorizeCodeCommand request,
        CancellationToken cancellationToken)
    {
        //if (!currentUser.IsAuthenticated)
        //{
        //    return ApplicationResult<string>.Failure("User not authenticated", "Please log in first");
        //}

        var requestScope = request.AuthorizeCodeRequestDto.Scope.Split(' ').ToList();
        var finalGrantedScopes = new List<string>();

        var client =  await clientRepository.GetClientAsync(request.AuthorizeCodeRequestDto.ClientId);
        if (client == null)
            return new ErrorApplicationResult($"Please check info again, ClientId: {request.AuthorizeCodeRequestDto.ClientId}");

        client.ValidateRedirectUri(request.AuthorizeCodeRequestDto.RedirectUri);

        if (client.RequirePkce && string.IsNullOrWhiteSpace(request.AuthorizeCodeRequestDto.CodeChallenge))
            return new ErrorApplicationResult("Code challenge is required for this client");
        
        var codeChallengeMethod = request.AuthorizeCodeRequestDto.CodeChallengeMethod;
        
        if (!string.IsNullOrWhiteSpace(request.AuthorizeCodeRequestDto.CodeChallenge))
        {
            if (string.IsNullOrWhiteSpace(codeChallengeMethod))
            {
                codeChallengeMethod = "plain"; 
            }
            
            var supportedMethods = new[] { "S256", "plain" };
            if (!supportedMethods.Contains(codeChallengeMethod))
                return new ErrorApplicationResult("Transform algorithm not supported. Server only supports S256 and plain");
        }

        //var user = await userRepository.GetUserInfoAsync(currentUser.UserId);
        var user = await userRepository.GetUserInfoAsync("user1@gmail.com");

        if (user == null)
            return new ErrorApplicationResult($"Please check info again, UserId: {currentUser.UserId}");

        var roles = user.Roles;

        foreach (var scope in requestScope.Where(scope => client.AllowedScopes.Contains(scope)))
        {
            if (roles != null && (IsIdentityScope(scope) || roles.Contains(scope)))
            {
                finalGrantedScopes.Add(scope);
            }
        }
        if (finalGrantedScopes.Count == 0)
        {
            return new ErrorApplicationResult("Please check info again, no scopes can be granted");
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
                          $"?code={authCode.Id}" +
                          $"&state={request.AuthorizeCodeRequestDto.State}" +
                          $"&scope={Uri.EscapeDataString(string.Join(" ", finalGrantedScopes))}";

        return new ValueApplicationResult<string>(redirectUrl);
    }
    
    private static bool IsIdentityScope(string scope)
    {
        return new[] { "openid", "profile", "email", "phone", "offline_access" }.Contains(scope);
    }
}