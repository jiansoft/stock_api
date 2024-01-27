using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.Revenue;

/// <summary>
/// 營收數據
/// </summary>
/// <param name="e"></param>
public class RevenueDto(RevenueEntity e)
{
    /// <summary>
    /// 股票編號
    /// </summary>
    public string StockSymbol { get; set; } = e.StockSymbol;

    /// <summary>
    /// 營收年月
    /// </summary>
    public long Date { get; set; } = e.Date;

    /// <summary>
    /// 當月營收
    /// </summary>
    public decimal Monthly { get; set; } = e.Monthly;

    /// <summary>
    /// 上月營收
    /// </summary>
    public decimal LastMonth { get; set; } = e.LastMonth;

    /// <summary>
    /// 去年同月營收
    /// </summary>
    public decimal LastYearThisMonth { get; set; } = e.LastYearThisMonth;

    /// <summary>
    /// 當月累積營收
    /// </summary>
    public decimal MonthlyAccumulated { get; set; } = e.MonthlyAccumulated;

    /// <summary>
    /// 去年當月累積營收
    /// </summary>
    public decimal LastYearMonthlyAccumulated { get; set; } = e.LastYearMonthlyAccumulated;

    /// <summary>
    /// 月增率%
    /// </summary>
    public decimal ComparedWithLastMonth { get; set; } = e.ComparedWithLastMonth;

    /// <summary>
    /// 年增率%
    /// </summary>
    public decimal ComparedWithLastYearSameMonth { get; set; } = e.ComparedWithLastYearSameMonth;

    /// <summary>
    /// 累計年增率%
    /// </summary>
    public decimal AccumulatedComparedWithLastYear { get; set; } = e.AccumulatedComparedWithLastYear;

    /// <summary>
    /// 月均價
    /// </summary>
    public decimal AvgPrice { get; set; } = e.AvgPrice;

    /// <summary>
    /// 當月最低價
    /// </summary>
    public decimal LowestPrice { get; set; } = e.LowestPrice;

    /// <summary>
    /// 當月最高價
    /// </summary>
    public decimal HighestPrice { get; set; } = e.HighestPrice;
}