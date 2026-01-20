using SharedKernel.Application.CQRS;
using SharedKernel.Application.Models;

namespace Identity.Application.Commands.GetAuthorizeCode;

public class GetAuthorizeCodeCommandHandler : ICommandHandler<GetAuthorizeCodeCommand, string>
{
    public async Task<Result<string>> Handle(GetAuthorizeCodeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}