namespace SharedKernel.Core.Abstractions;

public interface IMultiTenant
{
    Guid TenantId { get; }
}