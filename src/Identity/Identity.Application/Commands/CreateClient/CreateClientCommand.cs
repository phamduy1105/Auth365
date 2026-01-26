using SharedKernel.Application.CQRS;

namespace Identity.Application.Commands.CreateClient;

public sealed record CreateClientCommand(ClientCreatedRequestDto ClientCreatedRequestDto) : ICommand<int>;