using StockApi.Models.HttpTransactions.Stock.Revenue;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 表示用於營收請求的參數的結構
/// </summary>
/// <param name="req"></param>
internal struct RevenueParam(RevenueRequest req) : IKey
{
    /// <summary>
    /// 取得或設定基本鍵值
    /// </summary>
    private string BaseKey { get; set; } = req.KeyWithPrefix();

    /// <summary>
    /// 取得或設定頁碼
    /// </summary>
    public long PageIndex { get; set; } = req.RequestedPage;

    /// <summary>
    /// 取得或設定每頁筆數
    /// </summary>
    public long PageSize { get; set; } = req.RecordsPerPage;

    /// <summary>
    /// 取得或設定年月
    /// </summary>
    public long MonthOfYear { get; set; } = req.MonthOfYear;

    /// <summary>
    /// 取得或設定股票代號
    /// </summary>
    public string StockSymbol { get; set; } = req.StockSymbol;

    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(RevenueParam)}:{StockSymbol}-{MonthOfYear}-{PageIndex}-{PageSize}";
    }
}