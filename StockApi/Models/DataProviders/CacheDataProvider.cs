using Microsoft.Extensions.Caching.Memory;

namespace StockApi.Models.DataProviders;

/// <summary>
/// 提供快取數據的類別，使用 MemoryCache 來存儲數據。
/// </summary>
/// <param name="cache">用於緩存數據的 MemoryCache 實例。</param>
public class CacheDataProvider(IMemoryCache cache) : IDataProvider
{
    private readonly object _lockGetLockObject = new();

    /// <summary>
    /// 創建一個新的快取選項，設置相對過期時間。
    /// </summary>
    /// <param name="relative">相對過期時間。</param>
    /// <returns>MemoryCacheEntryOptions 實例。</returns>
    public static MemoryCacheEntryOptions NewOption(TimeSpan relative)
    {
        return new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
    }

    /// <summary>
    /// 獲取或設定快取數據。如果快取中已存在數據，則返回該數據；否則，使用工廠方法創建數據並存入快取。
    /// </summary>
    /// <typeparam name="T">數據的類型。</typeparam>
    /// <param name="key">快取的鍵。</param>
    /// <param name="option">快取選項。</param>
    /// <param name="factory">用於創建數據的工廠方法。</param>
    /// <returns>快取的數據。</returns>
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

    /// <summary>
    /// 刪除指定鍵的快取數據。
    /// </summary>
    /// <param name="key">要刪除的快取鍵。</param>
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