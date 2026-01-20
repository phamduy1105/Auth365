using Identity.Domain.Aggregates;

namespace Identity.Domain.Abstractions;

public interface IClientRepository
{
    public Client?  GetClient(string clientId);
    public Task<Client?> GetClientAsync(string clientId);
}