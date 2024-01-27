namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

/// <summary>
/// 用於封裝最後一次日報價的數據結構，包含日期、分頁元信息和數據列表。
/// </summary>
/// <param name="date">報價數據的日期。</param>
/// <param name="meta">分頁相關的元信息。</param>
/// <param name="data">包含特定類型報價數據的集合。</param>
/// <typeparam name="T">報價數據的類型。</typeparam>
public struct LastDailyQuotePayload<T>(string date, Meta meta, IEnumerable<T> data) : IPagingPayload<T>
{
    /// <summary>
    /// 報價數據的日期。
    /// </summary>
    public string Date { get; set; } = date;

    /// <summary>
    /// 分頁相關的元信息。
    /// </summary>
    public Meta Meta { get; set; } = meta;

    /// <summary>
    /// 包含特定類型報價數據的集合。
    /// </summary>
    public IEnumerable<T> Data { get; set; } = data;
}

/// <summary>
/// 用於封裝最後一次日報價響應的類別，包含報價數據負載。
/// </summary>
/// <param name="payload">報價數據的負載。</param>
/// <typeparam name="T">報價數據的類型。</typeparam>
public class LastDailyQuoteResponse<T>(T payload) : IResponse<T>
{
    /// <summary>
    /// 返回包含前綴的鍵值，用於唯一標識此響應。
    /// </summary>
    /// <returns>格式化的字符串，包含響應的標識信息。</returns>
    public string KeyWithPrefix()
    {
        return $"{nameof(LastDailyQuoteResponse<T>)}";
    }

    /// <summary>
    /// 響應的狀態碼。
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 包含的報價數據負載。
    /// </summary>
    public T Payload { get; set; } = payload;
}