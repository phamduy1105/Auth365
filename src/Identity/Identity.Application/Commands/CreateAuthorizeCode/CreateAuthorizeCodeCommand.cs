using SharedKernel.Application.CQRS;

namespace Identity.Application.Commands.CreateAuthorizeCode;

public sealed record CreateAuthorizeCodeCommand(AuthorizeCodeRequestDto AuthorizeCodeRequestDto) :
    ICommand<string>;
