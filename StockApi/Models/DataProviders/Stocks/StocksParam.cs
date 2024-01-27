using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 表示用於獲取股票數據的請求參數類別。
/// </summary>
/// <param name="req"></param>
public struct StocksParam(AbstractPagingRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();

    /// <summary>
    /// 請求的頁數
    /// </summary>
    public long PageIndex { get; set; } = req.RequestedPage;

    /// <summary>
    /// 每頁的記錄數
    /// </summary>
    public long PageSize { get; set; } = req.RecordsPerPage;

    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(StocksParam)}:{PageIndex}-{PageSize}";
    }
}