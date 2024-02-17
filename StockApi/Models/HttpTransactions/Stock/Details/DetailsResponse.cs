namespace StockApi.Models.HttpTransactions.Stock.Details;

/// <summary>
/// 表示用於回應詳細數據的泛型類別。
/// </summary>
/// <param name="Code"></param>
/// <param name="Payload"></param>
/// <typeparam name="T">數據類型</typeparam>
public record DetailsResponse<T>(int Code, T Payload) : IResponseV1<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(DetailsResponse<T>)}-{typeof(T).Name}";
    }
}