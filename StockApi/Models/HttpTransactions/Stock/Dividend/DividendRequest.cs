namespace StockApi.Models.HttpTransactions.Stock.Dividend;

/// <summary>
/// 表示用於獲取股票股利資訊的請求類別。
/// </summary>
/// <param name="stockSymbol">股票代碼。</param>
public class DividendRequest(string stockSymbol) : IRequest
{
    /// <summary>
    /// 股票代碼
    /// </summary>
    public string StockSymbol { get; } = stockSymbol;

    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(DividendRequest)}:{StockSymbol}";
    }
}