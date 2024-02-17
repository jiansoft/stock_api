using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Entities;

/// <summary>
/// 代表股市指數的實體類別，包含與股市指數相關的各項數據。
/// </summary>
public class IndexEntity
{
    /// <summary>
    /// 
    /// </summary>
    public long Serial { get; set; }
    
    /// <summary>
    /// 日期。
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [StringLength(255)]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// 交易量。
    /// </summary>
    public decimal TradingVolume { get; set; }

    /// <summary>
    /// 交易次數。
    /// </summary>
    public decimal Transaction { get; set; }

    /// <summary>
    /// 交易價值。
    /// </summary>
    public decimal TradeValue { get; set; }

    /// <summary>
    /// 變動量。
    /// </summary>
    public decimal Change { get; set; }

    /// <summary>
    /// 指數值。
    /// </summary>
    public decimal Index { get; set; }
}