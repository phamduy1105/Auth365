using SharedKernel.Application.CQRS;

namespace Identity.Application.Commands.CreateToken;

public sealed record CreateTokenCommand(TokenRequestDto TokenRequestDto) : ICommand<TokenResult>;