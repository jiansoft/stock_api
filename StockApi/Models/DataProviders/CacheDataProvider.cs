using jIAnSoft.Nami.Clockwork;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace StockApi.Models.DataProviders;

/// <summary>
/// 提供快取數據的類別，使用 MemoryCache 來存儲數據。
/// </summary>
public class CacheDataProvider : IDataProvider
{
    /// <summary>
    /// 用於緩存數據的 MemoryCache 實例。
    /// </summary>
    private readonly IMemoryCache _cache;
    
    /// <summary>
    /// 構造函數，初始化 CacheDataProvider 並設置定時清理信號量的任務。
    /// </summary>
    /// <param name="cache">IMemoryCache 實例，用於實際的資料快取。</param>
    public CacheDataProvider(IMemoryCache cache)
    {
        _cache = cache;
        Nami.Every(1).Hours().Do(CleanupSemaphores);
    }

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
    /// 非同步地獲取指定鍵的快取值。如果快取中不存在該值，則使用提供的方法非同步生成並設置該值。
    /// </summary>
    /// <param name="key">快取中用於檢索值的鍵。</param>
    /// <param name="option">設置快取項目的選項，如過期時間。</param>
    /// <param name="factory">用於非同步生成值的方法。</param>
    /// <typeparam name="T">快取值的類型。</typeparam>
    /// <returns>從快取中獲取的現有值，或者由方法新生成的值。</returns>
    public async Task<T> GetOrSetAsync<T>(string key, MemoryCacheEntryOptions option, Func<Task<T>> factory)
    {
        if (Get<T>(key) is { } cachedValue)
        {
            return cachedValue;
        }

        var semaphore = GetSemaphoreForKey(key,option);
        
        await semaphore.WaitAsync();

        try
        {
            if (Get<T>(key) is { } doubleCheckedCachedValue)
            {
                return doubleCheckedCachedValue;
            }
            
            var valueToCache = await factory.Invoke();
            
            Set(key, valueToCache, option);

            return valueToCache;
        }
        finally
        {
            semaphore.Release();
        }
    }

    private readonly ConcurrentDictionary<string, SemaphoreInfo> _semaphores = new();

    private SemaphoreSlim GetSemaphoreForKey(string key, MemoryCacheEntryOptions option)
    {
        var semaphoreInfo = _semaphores.GetOrAdd(key, _ => new SemaphoreInfo(new SemaphoreSlim(1, 1)));
        if (option.AbsoluteExpirationRelativeToNow != null)
        {
            semaphoreInfo.SlidingExpiration = DateTime.Now.Add(option.AbsoluteExpirationRelativeToNow.Value);
        }

        return semaphoreInfo.Semaphore;
    }

    /// <summary>
    /// 獲取或設定快取數據。如果快取中已存在數據，則返回該數據；否則，使用方法創建數據並存入快取。
    /// </summary>
    /// <typeparam name="T">數據的類型。</typeparam>
    /// <param name="key">快取的鍵。</param>
    /// <param name="option">快取選項。</param>
    /// <param name="factory">用於創建數據的方法。</param>
    /// <returns>快取的數據。</returns>
    public T GetOrSet<T>(string key, MemoryCacheEntryOptions option, Func<T> factory)
    {
        if (Get<T>(key) is { } cachedValue)
        {
            return cachedValue;
        }

        var semaphore = GetSemaphoreForKey(key, option);

        semaphore.Wait();
        
        try
        {
            if (Get<T>(key) is { } doubleCheckedCachedValue)
            {
                return doubleCheckedCachedValue;
            }

            var tempValue = factory.Invoke();

            Set(key, tempValue, option);

            return tempValue;
        }
        finally
        {
            semaphore.Release();
        }
    }

    private void Set<T>(string key, T val, MemoryCacheEntryOptions option)
    {
        _cache.Set(key, val, option);
    }

    private T? Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    /// <summary>
    /// 刪除指定鍵的快取數據。
    /// </summary>
    /// <param name="key">要刪除的快取鍵。</param>
    public void Remove(string key)
    {
        if (_semaphores.TryRemove(key, out var semaphoreInfo))
        {
            semaphoreInfo.Semaphore.Dispose();
        }
        
        _cache.Remove(key);
    }
    
    /// <summary>
    /// 定時清理過期的信號量，釋放資源。
    /// </summary>
    private void CleanupSemaphores()
    {
        var cutoff = DateTime.Now;
        var keysToRemove = _semaphores
            .Where(kvp => kvp.Value.SlidingExpiration < cutoff)
            .Select(kvp => kvp.Key);

        foreach (var key in keysToRemove)
        {
            if (_semaphores.TryRemove(key, out var semaphoreInfo))
            {
                semaphoreInfo.Semaphore.Dispose();
            }
        }
    }
}

/// <summary>
/// 
/// </summary>
/// <param name="semaphore"></param>
internal struct SemaphoreInfo( SemaphoreSlim semaphore)
{
    public SemaphoreSlim Semaphore { get; } = semaphore;
    public DateTime SlidingExpiration { get; set; } = DateTime.UtcNow;
}