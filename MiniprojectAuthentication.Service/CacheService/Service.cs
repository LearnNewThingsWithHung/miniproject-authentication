using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace MiniProjectAuthentication.Service.CacheService;

public class Service: IService
{
    private readonly IDistributedCache _distributedCache;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromHours(5);


    public Service(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    public async Task Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = expiration ?? _defaultExpiration
        };
        
        var jsonString = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key, jsonString, options);
    }   

    public async Task<T?> Get<T>(string key)
    {
        var jsonString = await _distributedCache.GetStringAsync(key);
        
        if (string.IsNullOrEmpty(jsonString))
        {
            return default;
        }
        return JsonSerializer.Deserialize<T?>(jsonString);
    }

    public async Task Remove(string key)
    { 
        await _distributedCache.RemoveAsync(key);
    }
}