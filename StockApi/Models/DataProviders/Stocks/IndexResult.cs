using StockApi.Models.Entities;
using StockApi.Models.HttpTransactions;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表索引結果的類別
/// </summary>
/// <param name="meta"></param>
/// <param name="entities"></param>
public class IndexResult(Meta meta, IEnumerable<IndexEntity> entities)
{
    /// <summary>
    /// 分頁的元數據
    /// </summary>
    public Meta Meta { get; set; } = meta;
    
    /// <summary>
    /// 取得或設定索引實體的集合
    /// </summary>
    public IEnumerable<IndexEntity> Entities { get; set; } = entities;
}