using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

namespace StockApi.Models.DataProviders.Stocks;

public struct HistoricalDailyQuoteParam(HistoricalDailyQuoteRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();
    public long PageIndex { get; set; } = req.PageIndex;
    public long PageSize { get; set; } = req.PageSize;
    public DateOnly Date  { get; set; } = req.Date;

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(HistoricalDailyQuoteParam)}:{Date}-{PageIndex}-{PageSize}";
    }
}