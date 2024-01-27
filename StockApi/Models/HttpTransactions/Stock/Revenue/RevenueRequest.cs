namespace StockApi.Models.HttpTransactions.Stock.Revenue;

/// <summary>
///  表示用於獲取收益數據的請求類別。
/// </summary>
/// <param name="requestedPage">請求的頁數</param>
/// <param name="recordsPerPage">每頁的記錄數</param>
public class RevenueRequest(int? requestedPage, int? recordsPerPage)
    : AbstractPagingRequest(requestedPage, recordsPerPage)
{
    /// <summary>
    /// 收益數據的所屬月份
    /// </summary>
    public int MonthOfYear { get; init; }

    /// <summary>
    /// 股票代號
    /// </summary>
    public string StockSymbol { get; init; } = string.Empty;

    /// <inheritdoc />
    public override string KeyWithPrefix()
    {
        return $"{nameof(RevenueRequest)}:{StockSymbol}-{MonthOfYear}-{RequestedPage}-{RecordsPerPage}";
    }
}