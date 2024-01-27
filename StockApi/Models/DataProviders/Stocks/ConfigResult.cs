using StockApi.Models.Entities;

namespace StockApi.Models.DataProviders.Stocks;

/// <summary>
/// 代表組態結果的類別
/// </summary>
/// <param name="entity"></param>
public class ConfigResult(ConfigEntity entity)
{
    /// <summary>
    /// 取得或設定組態實體
    /// </summary>
    public ConfigEntity Entity { get; set; } = entity;
}