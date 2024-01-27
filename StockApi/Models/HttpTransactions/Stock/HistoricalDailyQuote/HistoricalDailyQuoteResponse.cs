namespace StockApi.Models.HttpTransactions.Stock.HistoricalDailyQuote;

/// <summary>
/// 表示歷史每日報價回應的泛型類別。
/// </summary>
/// <typeparam name="T">回應的資料類型。</typeparam>
public class HistoricalDailyQuoteResponse<T>(T payload) : IResponse<T>
{
    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(HistoricalDailyQuoteResponse<T>)}";
    }

    /// <inheritdoc />
    public int Code { get; set; }

    /// <inheritdoc />
    public T Payload { get; set; } = payload;
}
