using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表歷史每日報價結果的類別
/// </summary>
/// <param name="meta"></param>
/// <param name="entities"></param>
/// <typeparam name="T"></typeparam>
public class HistoricalDailyQuoteResult<T>(Meta meta, T entities) : IDataResult<T>
{
    /// <summary>
    /// 分頁的元數據
    /// </summary>
    public Meta Meta { get; set; } = meta;

    /// <inheritdoc />
    public T Result { get; set; } = entities;
}