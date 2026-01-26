using Identity.Domain.Aggregates;
using SharedKernel.Application.CQRS;
using SharedKernel.Application.Models.Result;

namespace Identity.Application.Commands.CreateClient;

public sealed class CreateClientCommandHandler : ICommandHandler<CreateClientCommand, int>
{
    public async Task<ApplicationResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = Client.Create(request.ClientCreatedRequestDto.TenantId,
            request.ClientCreatedRequestDto.ClientId,
            request.ClientCreatedRequestDto.ClientUri,
            request.ClientCreatedRequestDto.ClientSecret,
            request.ClientCreatedRequestDto.RedirectUri,
            request.ClientCreatedRequestDto.PostLogoutRedirectUri);

        return await Task.FromResult(new ValueApplicationResult<int>(200));
    }
}