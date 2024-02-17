using StockApi.Models.Defines;

namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 定義一個包含數據的介面。
/// </summary>
/// <typeparam name="T">數據的類型。</typeparam>
public interface IPayload<T>
{
    /// <summary>
    /// 數據實體。
    /// </summary>
    public T Data { get; set; }
}

/// <summary>
/// 定義一個包含分頁數據的介面。
/// </summary>
/// <typeparam name="T">分頁數據的類型。</typeparam>
public interface IPagingPayload<T>
{
    /// <summary>
    /// 包含分頁相關資訊的元數據。
    /// </summary>
    public Meta Meta { get; set; } 
    
    /// <summary>
    /// 分頁數據的集合。
    /// </summary>
    public IEnumerable<T> Data { get; set; }
}

/// <summary>
/// 提供分頁功能的結構，包含元數據和數據集合。
/// </summary>
/// <param name="meta">分頁的元數據。</param>
/// <param name="data">數據集合。</param>
/// <typeparam name="T">數據的類型。</typeparam>
public struct PagingPayload<T>(Meta meta, IEnumerable<T> data) : IPagingPayload<T>
{
    /// <inheritdoc />
    public Meta Meta { get; set; } = meta;

    /// <inheritdoc />
    public IEnumerable<T> Data { get; set; } = data;
}

/// <summary>
/// 描述分頁查詢的元數據資訊。
/// </summary>
public record Meta
{
    /// <summary>
    /// 請求的頁碼。
    /// </summary>
    public long RequestedPage { get; set; }

    /// <summary>
    /// 每頁的記錄數。
    /// </summary>
    public long RecordsPerPage { get; set; }

    /// <summary>
    /// 總頁數。
    /// </summary>
    public long PageCount { get; set; }

    /// <summary>
    /// 總記錄數。
    /// </summary>
    public long RecordCount { get; set; }

    /// <summary>
    /// 建構函數，用於初始化分頁的元數據。
    /// </summary>
    /// <param name="recordCount">總記錄數。</param>
    /// <param name="requestedPage">請求的頁碼。</param>
    /// <param name="recordsPerPage">每頁的記錄數。</param>
    public Meta(long recordCount, long requestedPage, long recordsPerPage)
    {
        RecordCount = recordCount;

        if (RecordCount <= 0) return;

        // 計算總頁數
        PageCount = (int)Math.Ceiling(recordCount / (decimal)recordsPerPage);
        RequestedPage = Math.Min(Math.Max(requestedPage, Constants.DefaultRequestedPage), PageCount);
        RecordsPerPage = recordsPerPage;
    }
}