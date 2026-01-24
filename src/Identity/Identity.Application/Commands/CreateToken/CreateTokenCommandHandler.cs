using Identity.Application.Interfaces;
using SharedKernel.Application.CQRS;
using SharedKernel.Application.Models.Result;

namespace Identity.Application.Commands.CreateToken;

public sealed class CreateTokenCommandHandler(IAuthorizationCodeStore authorizationCodeStore) :
    ICommandHandler<CreateTokenCommand, TokenResult>
{
    public async Task<ApplicationResult> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var authorizationRequest = await  authorizationCodeStore.GetCodeAsync(request.TokenRequestDto.Code);
        if (authorizationRequest is null)
            return new ErrorApplicationResult("Authorization Code not found");

        var tokenResult = new TokenResult();
        return new ValueApplicationResult<TokenResult>(tokenResult);
    }
}