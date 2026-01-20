using SharedKernel.Application.CQRS;

namespace Identity.Application.Commands.GetAuthorizeCode;

public sealed record GetAuthorizeCodeCommand(AuthorizeCodeRequestDto AuthorizeCodeRequestDto) :
    ICommand<string>;
