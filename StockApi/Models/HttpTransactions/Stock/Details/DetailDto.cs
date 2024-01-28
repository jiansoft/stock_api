using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.Details;

/// <summary>
/// 表示用於表示股票詳細數據的數據傳輸對象（DTO）類別。
/// </summary>
/// <param name="se">股票實體</param>
public class DetailDto(StockEntity se)
{
    /// <summary>
    /// 初始化 <see cref="DetailDto"/> 類別的新執行個體。
    /// </summary>
    public DetailDto() : this(new StockEntity())
    {
    }

    /// <summary>
    /// 股票代號
    /// </summary>
    public string StockSymbol { get; set; } = se.StockSymbol;

    /// <summary>
    /// 名稱
    /// </summary>
    public string Name { get; set; } = se.Name;

    /// <summary>
    /// 上一季度每股盈餘（EPS）
    /// </summary>
    public decimal LastOneEps { get; set; } = se.LastOneEps;

    /// <summary>
    /// 過去四季每股盈餘（EPS）的總和
    /// </summary>
    public decimal LastFourEps { get; set; } = se.LastFourEps;

    /// <summary>
    /// 權重
    /// </summary>
    public decimal Weight { get; set; } = se.Weight;

    /// <summary>
    /// 行業編號
    /// </summary>
    public int IndustryId { get; set; } = se.IndustryId;

    /// <summary>
    /// 交易市場編號
    /// </summary>
    public int ExchangeMarketId { get; set; } = se.ExchangeMarketId;
}
