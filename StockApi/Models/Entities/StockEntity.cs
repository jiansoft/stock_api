namespace StockApi.Models.Entities;

/// <summary>
/// 代表股票實體的類別
/// </summary>
public class StockEntity
{
    /// <summary>
    /// 取得或設定交易市場的編號
    /// </summary>
    public int ExchangeMarketId { get; set; }
    
    /// <summary>
    /// 取得或設定行業的編號
    /// </summary>
    public int IndustryId { get; set; }

    /// <summary>
    /// 取得或設定股票代號
    /// </summary>
    public string StockSymbol { get; set; } = string.Empty;

    /// <summary>
    /// 取得或設定股票名稱
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 取得或設定最近一期的每股盈餘 (EPS)
    /// </summary>
    public decimal LastOneEps { get; set; }

    /// <summary>
    /// 取得或設定過去四期的每股盈餘 (EPS) 總和
    /// </summary>
    public decimal LastFourEps { get; set; }

    /// <summary>
    /// 代表物件的權重
    /// </summary>
    public decimal Weight { get; set; }
}