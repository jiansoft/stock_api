namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

public class HistoricalDailyQuoteRequest : IRequest
{
    public DateOnly Date { get; set; }
    public long PageIndex { get; set; }
    public long PageSize { get; set; }

    public string KeyWithPrefix()
    {
        return $"{nameof(HistoricalDailyQuoteRequest)}:{PageIndex}-{PageSize}";
    }
}