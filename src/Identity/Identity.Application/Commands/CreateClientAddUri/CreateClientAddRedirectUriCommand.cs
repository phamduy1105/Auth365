using SharedKernel.Application.CQRS;

namespace Identity.Application.Commands.CreateClientAddUri;

public record CreateClientAddRedirectUriCommand(Guid Id,
    string Value) : ICommand<int>;