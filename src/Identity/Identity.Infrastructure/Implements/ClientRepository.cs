using Identity.Domain.Abstractions;
using Identity.Domain.Aggregates;

namespace Identity.Infrastructure.Implements;

public sealed class ClientRepository : IClientRepository
{
    private readonly List<Client> _clients;
    public ClientRepository()
    {
        _ = Guid.TryParse("11111111-1111-1111-1111-111111111111", out var tenantId);
    
        var client1 = Client.Create(
            tenantId,
            "client_app",
            "https://app.example.com",
            "AAAAAAALALALWLWLWLDLDLWLDSLDSDLSLDWLDLADLALSDLASDL",
            "https://app.example.com/callback",
            "https://app.example.com/logout");
        client1.AddAllowedScope("product.view");
        client1.AddAllowedScope("product.delete");

        var client2 = Client.Create(
            tenantId,
            "client_mobile",
            "https://mobile.example.com",
            "AAAAAAALALALWLWLWLDLDLWLDSLDSDLSLDWLDLADLALSDLASDL",
            "https://mobile.example.com/callback",
            "https://mobile.example.com/logout");
        client2.AddAllowedScope("product.view");

        _clients =
        [
            client1,
            client2
        ];
    }
    public async Task<Client?> GetClientAsync(string clientId)
    {
        var client = _clients.FirstOrDefault(c => c.ClientId == clientId);
        return await Task.FromResult(client);
    }
}