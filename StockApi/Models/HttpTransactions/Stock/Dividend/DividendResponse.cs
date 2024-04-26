namespace StockApi.Models.HttpTransactions.Stock.Dividend;

/// <summary>
/// 表示股利數據的有效負載（Payload）對象。
/// </summary>
/// <param name="data"></param>
/// <typeparam name="T"></typeparam>
public class DividendPayload<T>(T data) : IPayload<T>
{
    /// <inheritdoc />
    public T Data { get; set; } = data;
}

/// <summary>
/// 表示股利回應的對象。
/// </summary>
/// <param name="code"></param>
/// <param name="data"></param>
/// <typeparam name="T"></typeparam>
public class DividendResponse<T>(int code,T data) : IResponse<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(DividendResponse<T>)}";
    }

    /// <inheritdoc />
    public int Code { get; set; } = code;

    /// <inheritdoc />
    public T Payload { get; set; } = data;
}