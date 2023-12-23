namespace StockApi.Models.HttpTransactions.Twse;


public struct TaiexPayload<T>(Meta meta, IEnumerable<T> data) : IPagingPayload<T>
{
    public Meta Meta { get; set; } = meta;
    public IEnumerable<T> Data { get; set; } = data;
}

public class TaiexResponse<T>(T payload) : IResponse<T>
{
    public string KeyWithPrefix()
    {
        return $"{nameof(TaiexResponse<T>)}";
    }

    public int Code { get; set; }

    public T Payload { get; set; } = payload;
}