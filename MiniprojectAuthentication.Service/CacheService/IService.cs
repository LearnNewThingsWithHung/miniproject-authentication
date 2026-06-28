namespace MiniProjectAuthentication.Service.CacheService;

public interface IService
{
    public Task Set<T>(string key, T value, TimeSpan? expiration = null);
    public Task<T?> Get<T>(string key);
    public Task Remove(string key);
}