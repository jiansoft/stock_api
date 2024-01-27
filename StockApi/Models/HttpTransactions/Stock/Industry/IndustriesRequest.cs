namespace StockApi.Models.HttpTransactions.Stock.Industry;

/// <summary>
/// 表示產業資訊請求的結構。
/// </summary>
public struct IndustriesRequest : IRequest
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(IndustriesRequest)}";
    }
}