namespace SharedKernel.Core.Abstractions;

public interface IAuditable
{
    public DateTimeOffset CreatedAt { get; }
    public Guid? CreatedBy { get; }
    public DateTimeOffset? LastModifiedAt { get; }
    public Guid? LastModifiedBy { get; }
}