using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
///  用於表示最後每日報價的參數結構。
/// </summary>
/// <param name="req"></param>
internal struct LastDailyQuoteParam(AbstractPagingRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();

    /// <summary>
    /// 取得或設定頁碼。
    /// </summary>
    public long PageIndex { get; set; } = req.RequestedPage;

    /// <summary>
    /// 取得或設定每頁筆數。
    /// </summary>
    public long PageSize { get; set; } = req.RecordsPerPage;

    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(LastDailyQuoteParam)}:{PageIndex}-{PageSize}";
    }
}