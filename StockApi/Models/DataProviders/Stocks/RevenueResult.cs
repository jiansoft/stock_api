using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 用來表示具有 Meta 資訊的資料結果的類別
/// </summary>
/// <param name="meta"></param>
/// <param name="rows"></param>
/// <typeparam name="T">實際的資料型別</typeparam>
internal class RevenueResult<T>(Meta meta, T rows) : IDataResult<T>
{
    /// <summary>
    /// 取得或設定 Meta 資訊
    /// </summary>
    public Meta Meta { get; set; } = meta;

    /// <inheritdoc />
    public T Rows { get; set; } = rows;
}