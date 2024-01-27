namespace StockApi.Models.HttpTransactions.Stock.Details;

/// <summary>
/// 表示用於回應詳細數據的泛型類別。
/// </summary>
/// <param name="payload"></param>
/// <typeparam name="T">數據類型</typeparam>
public class DetailsResponse<T>(T payload) : IResponse<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(DetailsResponse<T>)}";
    }

    /// <inheritdoc />
    public int Code { get; set; }

    /// <inheritdoc />
    public T Payload { get; set; } = payload;
}