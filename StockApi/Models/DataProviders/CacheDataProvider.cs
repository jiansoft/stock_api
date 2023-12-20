using Microsoft.Extensions.Caching.Memory;

namespace StockApi.Models.DataProviders;

public class CacheDataProvider(IMemoryCache cache) : IDataProvider
{
    private readonly object _lockGetLockObject = new();

    public static MemoryCacheEntryOptions NewOption(TimeSpan relative)
    {
        return new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
    }

    public T GetOrSet<T>(string key, MemoryCacheEntryOptions option, Func<T> factory)
    {
        if (Get<T>(key) is { } cachedValue)
        {
            return cachedValue;
        }

        var lockObject = GetLockObject(key);

        lock (lockObject)
        {
            if (Get<T>(key) is { } doubleCheckedCachedValue)
            {
                return doubleCheckedCachedValue;
            }
        }

        var tempValue = factory.Invoke();

        lock (lockObject)
        {
            Set(key, tempValue, option);
        }

        return tempValue;
    }

    private void Set<T>(string key, T val, MemoryCacheEntryOptions option)
    {
        cache.Set(key, val, option);
    }

    private T? Get<T>(string key)
    {
        return cache.Get<T>(key);
    }

    public void Remove(string key)
    {
        cache.Remove(key);
    }

    private object GetLockObject(string key)
    {
        var lockKey = $"CacheLocker:{key}";

        if (Get<object>(lockKey) is { } existingLock)
        {
            return existingLock;
        }

        lock (_lockGetLockObject)
        {
            if (Get<object>(lockKey) is { } doubleCheckedLock)
            {
                return doubleCheckedLock;
            }

            var newLockObject = new object();


            Set(lockKey, newLockObject, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(10)
            });
            return newLockObject;
        }
    }
}