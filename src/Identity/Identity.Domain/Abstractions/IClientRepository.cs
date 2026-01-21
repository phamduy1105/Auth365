using Identity.Domain.Aggregates;

namespace Identity.Domain.Abstractions;

public interface IClientRepository
{
    public Task<Client?> GetClientAsync(string clientId);
}