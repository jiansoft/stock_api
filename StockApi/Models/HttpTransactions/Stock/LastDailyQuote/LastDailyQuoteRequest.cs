namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

public struct LastDailyQuoteRequest: IRequest
{
    public long PageIndex { get; set; }
    public long PageSize { get; set; }
    public string KeyWithPrefix()
    {
        return $"{nameof(LastDailyQuoteRequest)}:{PageIndex}-{PageSize}";
    }
}