using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表股利結果的類別
/// </summary>
/// <param name="entities"></param>
internal class DividendResult(IEnumerable<DividendEntity> entities)
{
    /// <summary>
    /// 初始化股利結果的新執行個體
    /// </summary>
    public IEnumerable<DividendEntity> Entities { get; } = entities;
}