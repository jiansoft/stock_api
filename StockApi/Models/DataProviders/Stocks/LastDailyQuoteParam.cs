using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

namespace StockApi.Models.DataProviders.Stocks;

public struct LastDailyQuoteParam(LastDailyQuoteRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();
    public long PageIndex { get; set; } = req.PageIndex;
    public long PageSize { get; set; } = req.PageSize;

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(LastDailyQuoteParam)}:{PageIndex}-{PageSize}";
    }
}