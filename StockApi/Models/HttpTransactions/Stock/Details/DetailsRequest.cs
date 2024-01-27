namespace StockApi.Models.HttpTransactions.Stock.Details;

/// <summary>
/// 表示股票詳細數據請求的對象。
/// </summary>
/// <param name="requestedPage">請求的頁碼。</param>
/// <param name="recordsPerPage">每頁的記錄數。</param>
public class DetailsRequest(int? requestedPage, int? recordsPerPage)
    : AbstractPagingRequest(requestedPage, recordsPerPage)
{
    /// <inheritdoc />
    public override string KeyWithPrefix()
    {
        return $"{nameof(DetailsRequest)}:{RequestedPage}-{RecordsPerPage}";
    }
}