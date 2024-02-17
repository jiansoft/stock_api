namespace StockApi.Models.HttpTransactions.Twse;

/// <summary>
/// 
/// </summary>
/// <param name="Code"></param>
/// <param name="Payload"></param>
/// <typeparam name="T"></typeparam>
public record TaiexResponse<T>(int Code, T Payload) : IResponseV1<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(TaiexResponse<T>)}-{typeof(T).Name}";
    }
}