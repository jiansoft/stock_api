namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 定義一個通用響應介面，包含了響應的狀態碼和負載數據。
/// </summary>
/// <typeparam name="T">響應中負載數據的類型。</typeparam>
public interface IResponse<T> : IHttpTransaction
{
    /// <summary>
    /// 響應的狀態碼，用於表示請求處理的結果。
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 響應中包含的負載數據。
    /// </summary>
    public T Payload { get; set; }
}