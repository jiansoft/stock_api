using Microsoft.Extensions.Caching.Memory;

namespace StockApi.Models.DataProviders;
public class CacheDataProvider(IMemoryCache cache) : IDataProvider
{
    private readonly object _lockGetLockObject = new();

    public static MemoryCacheEntryOptions NewOption(TimeSpan relative)
    {
        return new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
    }
    
    public T GetOrSet<T>(string key, MemoryCacheEntryOptions option, Func<T> func)
    {
        var cacheValue = Get<T>(key);
        if (cacheValue != null)
        {
            return cacheValue;
        }

        lock (_lockGetLockObject)
        {
            cacheValue = Get<T>(key);

            if (cacheValue != null)
            {
                return cacheValue;
            }
            
            var tempValue = func.Invoke();
            
            Set(key, tempValue, option);
            
            return tempValue;
        }
    }

    public void Set<T>(string key, T val, MemoryCacheEntryOptions option)
    {
        cache.Set(key, val, option);
    }

    public T? Get<T>(string key)
    {
        var cacheItem = cache.Get<T>(key);
        return cacheItem;
    }
}