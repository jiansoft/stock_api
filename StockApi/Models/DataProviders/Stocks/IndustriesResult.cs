using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 用來表示產業資訊結果的類別
/// </summary>
/// <param name="entities"></param>
public class IndustriesResult(IEnumerable<StockIndustryEntity> entities)
{
    /// <summary>
    /// 取得或設定產業實體的集合
    /// </summary>
    public IEnumerable<StockIndustryEntity> Entities { get; } = entities;
}