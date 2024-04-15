using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApi.Models.Entities;

/// <summary>
/// 代表股票實體的類別
/// </summary>
[Table("stocks")]
public sealed class StockEntity : IEntity
{
    /// <summary>
    /// 取得或設定交易市場的編號
    /// </summary>
    [Column("stock_exchange_market_id", TypeName = "integer")]
    public int ExchangeMarketId { get; set; }

    /// <summary>
    /// 取得或設定行業的編號
    /// </summary>
    [Column("stock_industry_id", TypeName = "integer")]
    public int IndustryId { get; set; }

    /// <summary>
    /// 取得或設定股票代號
    /// </summary>
    [Key]
    public string StockSymbol { get; set; } = string.Empty;

    /// <summary>
    /// 取得或設定股票名稱
    /// </summary>
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 取得或設定最近一期的每股盈餘 (EPS)
    /// </summary>
    [Column(TypeName = "numeric(18,4)")]
    public decimal LastOneEps { get; set; }

    /// <summary>
    /// 取得或設定過去四期的每股盈餘 (EPS) 總和
    /// </summary>
    [Column(TypeName = "numeric(18,4)")]
    public decimal LastFourEps { get; set; }

    /// <summary>
    /// 代表物件的權重
    /// </summary>
    [Column(TypeName = "numeric(18,4)")]
    public decimal Weight { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public StockIndustryEntity? Industry { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public StockExchangeMarketEntity? ExchangeMarket { get; set; }
}