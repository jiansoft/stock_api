using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.LastDailyQuote;

public class LastDailyQuoteDto(LastDailyQuoteEntity ldqe)
{
    /// <summary>
    /// 市場編號
    /// </summary>
    public int StockExchangeMarketId { get; set; } = ldqe.StockExchangeMarketId;

    /// <summary>
    /// 股票代號
    /// </summary>
    public string StockSymbol { get; set; } = ldqe.StockSymbol;

    /// <summary>
    /// 成交量
    /// </summary>
    public decimal TradingVolume { get; set; } = ldqe.TradingVolume;

    /// <summary>
    /// 成交筆數
    /// </summary>
    public int Transaction { get; set; } = Convert.ToInt32(ldqe.Transaction);

    /// <summary>
    /// 成交值
    /// </summary>
    public decimal TradeValue { get; set; } = ldqe.TradeValue;

    /// <summary>
    /// 開盤價
    /// </summary>
    public decimal OpeningPrice { get; set; } = ldqe.OpeningPrice;

    /// <summary>
    /// 最高價
    /// </summary>
    public decimal HighestPrice { get; set; } = ldqe.HighestPrice;

    /// <summary>
    /// 最低價
    /// </summary>
    public decimal LowestPrice { get; set; } = ldqe.LowestPrice;

    /// <summary>
    /// 收盤價
    /// </summary>
    public decimal ClosingPrice { get; set; } = ldqe.ClosingPrice;

    /// <summary>
    /// 漲跌幅
    /// </summary>
    public decimal ChangeRange { get; set; } = ldqe.ChangeRange;

    /// <summary>
    /// 漲跌
    /// </summary>
    public decimal Change { get; set; } = ldqe.Change;

    /// <summary>
    /// 五日線(周線)
    /// </summary>
    public decimal MovingAverage5 { get; set; } = ldqe.MovingAverage5;

    /// <summary>
    /// 10日線(雙周線)
    /// </summary>
    public decimal MovingAverage10 { get; set; } = ldqe.MovingAverage10;

    /// <summary>
    /// 20日線(月線)
    /// </summary>
    public decimal MovingAverage20 { get; set; } = ldqe.MovingAverage20;

    /// <summary>
    /// 60日線(季線)
    /// </summary>
    public decimal MovingAverage60 { get; set; } = ldqe.MovingAverage60;

    /// <summary>
    /// 120日線(半年線)
    /// </summary>
    public decimal MovingAverage120 { get; set; } = ldqe.MovingAverage120;

    /// <summary>
    /// 240日線(年線)
    /// </summary>
    public decimal MovingAverage240 { get; set; } = ldqe.MovingAverage240;

    /// <summary>
    /// 年內最高價
    /// </summary>
    public decimal MaximumPriceInYear { get; set; } = ldqe.MaximumPriceInYear;

    /// <summary>
    /// 年內最低價
    /// </summary>
    public decimal MinimumPriceInYear { get; set; } = ldqe.MaximumPriceInYear;

    /// <summary>
    /// 年內平均價
    /// </summary>
    public decimal AveragePriceInYear { get; set; } = ldqe.AveragePriceInYear;

    /// <summary>
    /// 那日為年內最高價
    /// </summary>
    public DateTime MaximumPriceInYearDateOn { get; set; } = ldqe.MaximumPriceInYearDateOn;

    /// <summary>
    ///  那日為年內最低價
    /// </summary>
    public DateTime MinimumPriceInYearDateOn { get; set; } = ldqe.MinimumPriceInYearDateOn;

    /// <summary>
    /// 股價淨值比
    /// </summary>
    public decimal PriceToBookRatio { get; set; } = ldqe.PriceToBookRatio;
}