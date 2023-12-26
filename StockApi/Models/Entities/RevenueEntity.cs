namespace StockApi.Models.Entities;

public class RevenueEntity
{
    public string StockSymbol { get; set; } = string.Empty;

    /// <summary>
    /// 營收年月
    /// </summary>
    public long Date { get; set; }

    /// <summary>
    /// 當月營收
    /// </summary>
    public decimal Monthly { get; set; }

    /// <summary>
    /// 上月營收
    /// </summary>
    public decimal LastMonth { get; set; }

    /// <summary>
    /// 去年同月營收
    /// </summary>
    public decimal LastYearThisMonth { get; set; }

    /// <summary>
    /// 當月累積營收
    /// </summary>
    public decimal MonthlyAccumulated { get; set; }

    /// <summary>
    /// 去年當月累積營收
    /// </summary>
    public decimal LastYearMonthlyAccumulated { get; set; }

    /// <summary>
    /// 月增率%
    /// </summary>
    public decimal ComparedWithLastMonth { get; set; }

    /// <summary>
    /// 年增率%
    /// </summary>
    public decimal ComparedWithLastYearSameMonth { get; set; }

    /// <summary>
    /// 累計年增率%
    /// </summary>
    public decimal AccumulatedComparedWithLastYear { get; set; }

    /// <summary>
    /// 月均價
    /// </summary>
    public decimal AvgPrice { get; set; }

    /// <summary>
    /// 當月最低價
    /// </summary>
    public decimal LowestPrice { get; set; }

    /// <summary>
    /// 當月最高價
    /// </summary>
    public decimal HighestPrice { get; set; }
}