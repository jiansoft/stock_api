namespace StockApi.Models.HttpTransactions;

/// <summary>
/// 定義一個包含數據的介面。
/// </summary>
/// <typeparam name="T">數據的類型。</typeparam>
internal interface IPayload<T>
{
    /// <summary>
    /// 數據實體。
    /// </summary>
    T Data { get; set; }
}