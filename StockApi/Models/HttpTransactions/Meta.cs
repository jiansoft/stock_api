using StockApi.Models.Defines;

namespace StockApi.Models.HttpTransactions;

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