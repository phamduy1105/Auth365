using Identity.Application.Interfaces;
using Identity.Domain.Aggregates;
using Microsoft.Extensions.Caching.Memory;

namespace Identity.Infrastructure.Implements;

public sealed class InMemoryAuthorizationCodeStore(IMemoryCache cache) : IAuthorizationCodeStore
{
    public Task StoreCodeAsync(string key, AuthorizationCode authorizationCode)
    {
        cache.Set(key, authorizationCode, new DateTimeOffset(DateTime.UtcNow.AddMinutes(5)));
        return Task.CompletedTask;
    }

    public async Task<AuthorizationCode?> GetCodeAsync(string key)
    {
        cache.TryGetValue(key, out AuthorizationCode? value);
        return await Task.FromResult(value);
    }

    public Task RemoveCodeAsync(string key)
    {
        cache.Remove(key);
        return Task.CompletedTask;
    }
}