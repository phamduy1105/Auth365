using MediatR;

namespace SharedKernel.Core.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTimeOffset OccurredOn { get; }
}