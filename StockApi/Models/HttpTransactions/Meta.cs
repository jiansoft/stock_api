using StockApi.Models.Defines;

namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 描述分頁查詢的元數據資訊。
/// </summary>
public record Meta
{
    /// <summary>
    /// 目前顯示的頁碼。
    /// </summary>
    public uint CurrentPage { get; }

    /// <summary>
    /// 於資料庫查詢時顯示第幾頁的索引，實際為 CurrentPage -1 ，最小值 0
    /// </summary>
    private int PageIndex => (int)(CurrentPage - 1);

    /// <summary>
    /// 於資料庫查詢時指定回傳幾筆數據
    /// </summary>
    internal int PageSize => (int)RecordsPerPage;

    /// <summary>
    /// 資料查詢時的偏移量 (meta.CurrentPage - 1) * meta.RecordsPerPage
    /// </summary>
    internal int Offset => PageIndex * PageSize;

    /// <summary>
    /// 每頁顯示幾筆數據。
    /// </summary>
    public uint RecordsPerPage { get; }

    /// <summary>
    /// 總頁數。
    /// </summary>
    public uint TotalPages { get; }

    /// <summary>
    /// 總記錄數。
    /// </summary>
    public ulong TotalRecords { get; }

    /// <summary>
    /// 建構函數，用於初始化分頁的元數據。
    /// </summary>
    /// <param name="totalRecords">總記錄數。</param>
    /// <param name="requestedPage">請求的頁碼。</param>
    /// <param name="recordsPerPage">每頁的記錄數。</param>
    public Meta(long totalRecords, uint requestedPage, uint recordsPerPage)
    {
        TotalRecords = (ulong)totalRecords;
        RecordsPerPage = recordsPerPage == Constants.Zero ? Constants.DefaultRecordsPerPage : recordsPerPage;
        // 計算總頁數
        TotalPages = CalculateTotalPages(totalRecords, recordsPerPage);
        // 確保請求的頁碼在合理範圍內
        CurrentPage = CalculateCurrentPage(requestedPage, TotalPages);
    }

    private static uint CalculateTotalPages(long totalRecords, uint recordsPerPage) =>
        (uint)(totalRecords > Constants.Zero
            ? Math.Ceiling((decimal)totalRecords / recordsPerPage)
            : Constants.Zero);

    private static uint CalculateCurrentPage(uint requestedPage, uint totalPages) =>
        totalPages > Constants.Zero
            ? Math.Clamp(requestedPage, Constants.DefaultPage, totalPages)
            : Constants.DefaultPage;
}