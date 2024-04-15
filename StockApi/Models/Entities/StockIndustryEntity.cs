namespace StockApi.Models.Entities;

/// <summary>
/// 產業實體
/// </summary>
public sealed class StockIndustryEntity : IEntity
{
    /// <summary>
    /// 取得或設定產業編號
    /// </summary>
    public int IndustryId { get; set; }

    /// <summary>
    /// 取得或設定產業名稱
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public ICollection<StockEntity> Stocks { get; set; } = new List<StockEntity>();
}