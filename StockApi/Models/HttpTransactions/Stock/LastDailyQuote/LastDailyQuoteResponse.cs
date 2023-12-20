
namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

public struct LastDailyQuotePayload<T>(string date, Meta meta, IEnumerable<T> data) : IPagingPayload<T>
{
    public string Date { get; set; } = date;
    public Meta Meta { get; set; } = meta;
    public IEnumerable<T> Data { get; set; } = data;
}

public class LastDailyQuoteResponse<T>(T payload) : IResponse<T>
{
    public string KeyWithPrefix()
    {
        return $"{nameof(LastDailyQuoteResponse<T>)}";
    }

    public int Code { get; set; }

    public T Payload { get; set; } = payload;
}