namespace StockApi.Models.Entities;

public class DailyQuoteEntity
{
    /// <summary>
    /// 市場編號
    /// </summary>
    public int StockExchangeMarketId { get; set; }

    /// <summary>
    /// 股票代號
    /// </summary>
    public string StockSymbol { get; set; } = string.Empty;

    /// <summary>
    /// 成交量
    /// </summary>
    public decimal TradingVolume { get; set; }

    /// <summary>
    /// 成交筆數
    /// </summary>
    public decimal Transaction { get; set; }

    /// <summary>
    /// 成交值
    /// </summary>
    public decimal TradeValue { get; set; }

    /// <summary>
    /// 開盤價
    /// </summary>
    public decimal OpeningPrice { get; set; }

    /// <summary>
    /// 最高價
    /// </summary>
    public decimal HighestPrice { get; set; }

    /// <summary>
    /// 最低價
    /// </summary>
    public decimal LowestPrice { get; set; }

    /// <summary>
    /// 收盤價
    /// </summary>
    public decimal ClosingPrice { get; set; }

    /// <summary>
    /// 漲跌幅
    /// </summary>
    public decimal ChangeRange { get; set; }

    /// <summary>
    /// 漲跌
    /// </summary>
    public decimal Change { get; set; }

    /// <summary>
    /// 五日線(周線)
    /// </summary>
    public decimal MovingAverage5 { get; set; }

    /// <summary>
    /// 10日線(雙周線)
    /// </summary>
    public decimal MovingAverage10 { get; set; }

    /// <summary>
    /// 20日線(月線)
    /// </summary>
    public decimal MovingAverage20 { get; set; }

    /// <summary>
    /// 60日線(季線)
    /// </summary>
    public decimal MovingAverage60 { get; set; }

    /// <summary>
    /// 120日線(半年線)
    /// </summary>
    public decimal MovingAverage120 { get; set; }

    /// <summary>
    /// 240日線(年線)
    /// </summary>
    public decimal MovingAverage240 { get; set; }

    /// <summary>
    /// 年內最高價
    /// </summary>
    public decimal MaximumPriceInYear { get; set; }

    /// <summary>
    /// 年內最低價
    /// </summary>
    public decimal MinimumPriceInYear { get; set; }

    /// <summary>
    /// 年內平均價
    /// </summary>
    public decimal AveragePriceInYear { get; set; }

    /// <summary>
    /// 那日為年內最高價
    /// </summary>
    public DateTime MaximumPriceInYearDateOn { get; set; }

    /// <summary>
    ///  那日為年內最低價
    /// </summary>
    public DateTime MinimumPriceInYearDateOn { get; set; }

    /// <summary>
    /// 股價淨值比
    /// </summary>
    public decimal PriceToBookRatio { get; set; }
}