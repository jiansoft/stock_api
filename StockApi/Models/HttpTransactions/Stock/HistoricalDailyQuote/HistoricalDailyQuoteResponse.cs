namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

public class HistoricalDailyQuoteResponse<T>(T payload) : IResponse<T>
{
    public string KeyWithPrefix()
    {
        return $"{nameof(HistoricalDailyQuoteResponse<T>)}";
    }

    public int Code { get; set; }

    public T Payload { get; set; } = payload;
}
