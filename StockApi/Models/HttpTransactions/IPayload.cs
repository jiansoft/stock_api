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

/// <summary>
/// 定義一個包含分頁數據的介面。
/// </summary>
/// <typeparam name="T">分頁數據的類型。</typeparam>
internal interface IPagingPayload<T>
{
    /// <summary>
    /// 包含分頁相關資訊的元數據。
    /// </summary>
    Meta Meta { get; set; }

    /// <summary>
    /// 分頁數據的集合。
    /// </summary>
    IEnumerable<T> Data { get; set; }
}