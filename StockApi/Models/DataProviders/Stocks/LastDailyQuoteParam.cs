using StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

namespace StockApi.Models.DataProviders.Stocks;

public struct LastDailyQuoteParam : IKey
{
    private string BaseKey { get; set; }
    public long PageIndex { get; set; }
    public long PageSize { get; set; }

    public LastDailyQuoteParam(LastDailyQuoteRequest req)
    {
        BaseKey = req.KeyWithPrefix();
        PageIndex = req.PageIndex;
        PageSize = req.PageSize;
    }

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(LastDailyQuoteParam)}";
    }
}