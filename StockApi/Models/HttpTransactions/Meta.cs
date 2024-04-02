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
        RecordsPerPage = recordsPerPage <= 0 ? Constants.DefaultRecordsPerPage : recordsPerPage;

        // 如果沒有記錄，直接返回
        if (recordCount <= 0)
        {
            PageCount = Constants.Zero;
            RequestedPage = Constants.DefaultRequestedPage;
            return;
        }

        // 計算總頁數
        PageCount = (long)Math.Ceiling((decimal)recordCount / recordsPerPage);

        // 確保請求的頁碼在合理範圍內
        RequestedPage = Math.Clamp(requestedPage, Constants.DefaultRequestedPage, PageCount);
    }
}