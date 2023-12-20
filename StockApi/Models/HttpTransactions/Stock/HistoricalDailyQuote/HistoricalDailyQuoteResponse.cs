namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

public struct HistoricalDailyQuotePayload<T>(Meta meta, IEnumerable<T> data) : IPagingPayload<T>
{
    public Meta Meta { get; set; } = meta;
    public IEnumerable<T> Data { get; set; } = data;
}

public class HistoricalDailyQuoteResponse<T>(T payload) : IResponse<T>
{
    public string KeyWithPrefix()
    {
        return $"{nameof(HistoricalDailyQuoteResponse<T>)}";
    }

    public int Code { get; set; }

    public T Payload { get; set; } = payload;
}
