using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 表示用於表示股票查詢結果的類別。
/// </summary>
/// <param name="meta">元數據</param>
/// <param name="entities">股票實體集合</param>
public class StocksResult(Meta meta, IEnumerable<StockEntity> entities)
{
    /// <summary>
    /// 分頁的元數據
    /// </summary>
    public Meta Meta { get; set; } = meta;
    
    /// <summary>
    /// 股票實體集合
    /// </summary>
    public IEnumerable<StockEntity> Entities { get; set; } = entities;
}