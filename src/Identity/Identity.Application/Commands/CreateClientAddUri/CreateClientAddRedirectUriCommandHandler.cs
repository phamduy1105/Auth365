using Identity.Domain.Abstractions;
using SharedKernel.Application.CQRS;
using SharedKernel.Application.Models.Result;

namespace Identity.Application.Commands.CreateClientAddUri;

public class CreateClientAddRedirectUriCommandHandler(IClientRepository clientRepository) :
    ICommandHandler<CreateClientAddRedirectUriCommand, int>
{
    public async Task<ApplicationResult> Handle(CreateClientAddRedirectUriCommand request, CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetClientAsync(request.Id);
        
        if (client is null)
            return new ErrorApplicationResult($"Client not found: {request.Id}");
        
        client.AddRedirectUri(request.Value);
        return new ValueApplicationResult<int>(200);
    }
}