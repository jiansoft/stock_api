using StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

namespace StockApi.Models.DataProviders.Stocks;

internal struct HistoricalDailyQuoteParam(HistoricalDailyQuoteRequest req) : IKey
{
    private string BaseKey { get; set; } = req.KeyWithPrefix();
    public int PageIndex { get; set; } = req.RequestedPage;
    public int PageSize { get; set; } = req.RecordsPerPage;
    public DateOnly Date  { get; set; } = req.Date;

    public string KeyWithPrefix()
    {
        return $"{BaseKey}:{nameof(HistoricalDailyQuoteParam)}:{Date}-{PageIndex}-{PageSize}";
    }
}