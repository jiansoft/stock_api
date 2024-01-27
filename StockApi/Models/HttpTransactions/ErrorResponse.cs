namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 定義一個錯誤回應類別，用於封裝和傳遞錯誤訊息。
/// </summary>
/// <param name="msg">包含的錯誤訊息或相關數據。</param>
/// <typeparam name="T">錯誤訊息或相關數據的類型。</typeparam>
public class ErrorResponse<T>(T msg) : IResponse<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(ErrorResponse<T>)}";
    }

    
    /// <inheritdoc />
    public int Code { get; set; }

    /// <inheritdoc />
    public T Payload { get; set; } = msg;
}