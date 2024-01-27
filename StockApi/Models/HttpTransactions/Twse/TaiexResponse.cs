namespace StockApi.Models.HttpTransactions.Twse;

/// <summary>
/// 
/// </summary>
/// <param name="meta"></param>
/// <param name="data"></param>
/// <typeparam name="T"></typeparam>
public struct TaiexPayload<T>(Meta meta, IEnumerable<T> data) : IPagingPayload<T>
{
    /// <inheritdoc />
    public Meta Meta { get; set; } = meta;

    /// <inheritdoc />
    public IEnumerable<T> Data { get; set; } = data;
}

/// <summary>
/// 
/// </summary>
/// <param name="payload"></param>
/// <typeparam name="T"></typeparam>
public class TaiexResponse<T>(T payload) : IResponse<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(TaiexResponse<T>)}";
    }

    /// <inheritdoc />
    public int Code { get; set; }

    /// <inheritdoc />
    public T Payload { get; set; } = payload;
}