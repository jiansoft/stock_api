using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Entities;

/// <summary>
/// 交易市場
/// </summary>
public sealed class StockExchangeMarketEntity : IEntity
{
    /// <summary>
    /// 交易所的市場編號
    /// </summary>
    public int StockExchangeMarketId { get; set; }
    
    /// <summary>
    /// 交易所的編號參考 stock_exchange
    /// </summary>
    public int StockExchangeId { get; set; }

    /// <summary>
    /// 交易所的市場代碼 TAI:上市 TWO:上櫃 TWE:興櫃
    /// </summary>
    public string Code { get; set; } = string.Empty;
    
    /// <summary>
    /// 取得或設交易所名稱
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// 股票
    /// </summary>
    public ICollection<StockEntity> Stocks { get; set; } = new List<StockEntity>();
}