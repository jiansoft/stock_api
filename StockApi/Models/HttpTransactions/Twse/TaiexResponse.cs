namespace StockApi.Models.HttpTransactions.Twse;

/// <summary>
/// 
/// </summary>
/// <param name="code"></param>
/// <param name="payload"></param>
/// <typeparam name="T"></typeparam>
public class TaiexResponse<T>(int code, T payload) : AbstractPagingResponse<T>(code, payload)
{
    /// <inheritdoc />
    public override string KeyWithPrefix()
    {
        return $"{nameof(TaiexResponse<T>)}-{typeof(T).Name}";
    }
}