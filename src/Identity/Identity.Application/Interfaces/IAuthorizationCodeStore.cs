using Identity.Domain.Aggregates;

namespace Identity.Application.Interfaces;

public interface IAuthorizationCodeStore
{
    public Task StoreCodeAsync(string key, AuthorizationCode authorizationCode);
    public Task<AuthorizationCode?> GetCodeAsync(string key);
    public Task RemoveCodeAsync(string key);
}