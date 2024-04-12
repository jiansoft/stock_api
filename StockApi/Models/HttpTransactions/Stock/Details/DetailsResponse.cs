namespace StockApi.Models.HttpTransactions.Stock.Details;

/// <summary>
/// 表示用於回應詳細數據的泛型類別。
/// </summary>
/// <param name="code"></param>
/// <param name="payload"></param>
/// <typeparam name="T">數據類型</typeparam>
public class DetailsResponse<T>(int code, T payload) : AbstractPagingResponse<T>(code, payload)
{
    /// <inheritdoc />
    public override string KeyWithPrefix()
    {
        return $"{nameof(DetailsResponse<T>)}-{typeof(T).Name}";
    }
}