namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 定義一個錯誤回應類別，用於封裝和傳遞錯誤訊息。
/// </summary>
/// <param name="Code">包含的錯誤碼。</param>
/// <param name="Message">包含的錯誤訊息或相關數據。</param>
internal record ErrorResponse(int Code, string Message) : IHttpTransaction
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(ErrorResponse)}";
    }
}