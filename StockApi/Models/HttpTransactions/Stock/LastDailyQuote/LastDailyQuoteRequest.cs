namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

/// <summary>
/// 用於取得最後的收盤股價封裝分頁請求的詳細資訊。
/// </summary>
/// <param name="requestedPage"></param>
/// <param name="recordsPerPage"></param>
public class LastDailyQuoteRequest(int? requestedPage, int? recordsPerPage)
    : AbstractPagingRequest(requestedPage, recordsPerPage)
{
    /// <summary>
    /// 返回包含前綴的鍵值，用於唯一標識該分頁請求。
    /// </summary>
    /// <returns></returns>
    public override string KeyWithPrefix()
    {
        return $"{nameof(LastDailyQuoteRequest)}:{RequestedPage}-{RecordsPerPage}";
    }
}