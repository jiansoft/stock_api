namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

/// <summary>
/// 表示歷史每日報價的請求類別。
/// </summary>
/// <param name="page">要求的頁碼。</param>
/// <param name="recordsPerPage">每頁的記錄數。</param>
public class HistoricalDailyQuoteRequest(uint? requestedPage, int? recordsPerPage)
    : AbstractPagingRequest(requestedPage, recordsPerPage)
{
    /// <summary>
    /// 請求的日期。
    /// </summary>
    public DateOnly Date { get; set; }

    /// <inheritdoc />
    public override string KeyWithPrefix()
    {
        return $"{nameof(HistoricalDailyQuoteRequest)}:{Date}-{RequestedPage}-{RecordsPerPage}";
    }
}