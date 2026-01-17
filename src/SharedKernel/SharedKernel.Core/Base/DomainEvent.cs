using SharedKernel.Core.Abstractions;

namespace SharedKernel.Core.Base;

public abstract class DomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}