using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.DtoBase;

/// <summary>
/// 表示每日報價的基礎數據傳輸對象。
/// </summary>
/// <param name="dqe"></param>
public abstract class DailyQuoteDtoBase(DailyQuoteEntity dqe)
{
    /// <summary>
    /// 市場編號
    /// </summary>
    public int StockExchangeMarketId { get; set; } = dqe.StockExchangeMarketId;

    /// <summary>
    /// 股票代號
    /// </summary>
    public string StockSymbol { get; set; } = dqe.StockSymbol;

    /// <summary>
    /// 成交量
    /// </summary>
    public decimal TradingVolume { get; set; } = dqe.TradingVolume;

    /// <summary>
    /// 成交筆數
    /// </summary>
    public int Transaction { get; set; } = Convert.ToInt32(dqe.Transaction);

    /// <summary>
    /// 成交值
    /// </summary>
    public decimal TradeValue { get; set; } = dqe.TradeValue;

    /// <summary>
    /// 開盤價
    /// </summary>
    public decimal OpeningPrice { get; set; } = dqe.OpeningPrice;

    /// <summary>
    /// 最高價
    /// </summary>
    public decimal HighestPrice { get; set; } = dqe.HighestPrice;

    /// <summary>
    /// 最低價
    /// </summary>
    public decimal LowestPrice { get; set; } = dqe.LowestPrice;

    /// <summary>
    /// 收盤價
    /// </summary>
    public decimal ClosingPrice { get; set; } = dqe.ClosingPrice;

    /// <summary>
    /// 漲跌幅
    /// </summary>
    public decimal ChangeRange { get; set; } = dqe.ChangeRange;

    /// <summary>
    /// 漲跌
    /// </summary>
    public decimal Change { get; set; } = dqe.Change;

    /// <summary>
    /// 五日線(周線)
    /// </summary>
    public decimal MovingAverage5 { get; set; } = dqe.MovingAverage5;

    /// <summary>
    /// 10日線(雙周線)
    /// </summary>
    public decimal MovingAverage10 { get; set; } = dqe.MovingAverage10;

    /// <summary>
    /// 20日線(月線)
    /// </summary>
    public decimal MovingAverage20 { get; set; } = dqe.MovingAverage20;

    /// <summary>
    /// 60日線(季線)
    /// </summary>
    public decimal MovingAverage60 { get; set; } = dqe.MovingAverage60;

    /// <summary>
    /// 120日線(半年線)
    /// </summary>
    public decimal MovingAverage120 { get; set; } = dqe.MovingAverage120;

    /// <summary>
    /// 240日線(年線)
    /// </summary>
    public decimal MovingAverage240 { get; set; } = dqe.MovingAverage240;

    /// <summary>
    /// 年內最高價
    /// </summary>
    public decimal MaximumPriceInYear { get; set; } = dqe.MaximumPriceInYear;

    /// <summary>
    /// 年內最低價
    /// </summary>
    public decimal MinimumPriceInYear { get; set; } = dqe.MaximumPriceInYear;

    /// <summary>
    /// 年內平均價
    /// </summary>
    public decimal AveragePriceInYear { get; set; } = dqe.AveragePriceInYear;

    /// <summary>
    /// 那日為年內最高價
    /// </summary>
    public DateOnly MaximumPriceInYearDateOn { get; set; } = DateOnly.FromDateTime(dqe.MaximumPriceInYearDateOn);

    /// <summary>
    ///  那日為年內最低價
    /// </summary>
    public DateOnly MinimumPriceInYearDateOn { get; set; } = DateOnly.FromDateTime(dqe.MinimumPriceInYearDateOn);

    /// <summary>
    /// 股價淨值比
    /// </summary>
    public decimal PriceToBookRatio { get; set; } = dqe.PriceToBookRatio;
}