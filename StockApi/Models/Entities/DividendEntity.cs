namespace StockApi.Models.Entities;

/// <summary>
/// 股利
/// </summary>
public class DividendEntity
{
    /// <summary>
    /// 除息日
    /// </summary>
    public string ExDividendDate1 { get; set; } = string.Empty;

    /// <summary>
    /// 除權日
    /// </summary>
    public string ExDividendDate2 { get; set; } = string.Empty;

    /// <summary>
    /// 現金股利發放日
    /// </summary>
    public string PayableDate1 { get; set; } = string.Empty;

    /// <summary>
    /// 股票股利發放日
    /// </summary>
    public string PayableDate2 { get; set; } = string.Empty;

    /// <summary>
    /// 股票代號
    /// </summary>
    public string StockSymbol { get; set; } = string.Empty;

    /// <summary>
    /// 股利所屬季度 空字串:全年度 Q1~Q4:第一季~第四季 H1~H2︰上半季~下半季
    /// </summary>
    public string Quarter { get; set; } = string.Empty;

    /// <summary>
    /// 股利發放年度
    /// </summary>
    public long Year { get; set; }

    /// <summary>
    /// 股利所屬年度
    /// </summary>
    public long YearOfDividend { get; set; }

    /// <summary>
    /// 股利總計
    /// </summary>
    public decimal Sum { get; set; }

    /// <summary>
    /// 股票股利
    /// </summary>
    public decimal StockDividend { get; set; }

    /// <summary>
    /// 現金股利
    /// </summary>
    public decimal CashDividend { get; set; }

    /// <summary>
    /// 盈餘分配率_現金
    /// </summary>
    public decimal PayoutRatioCash { get; set; }

    /// <summary>
    /// 盈餘分配率_股要(%)
    /// </summary>
    public decimal PayoutRatioStock { get; set; }

    /// <summary>
    /// 盈餘分配率(%)
    /// </summary>
    public decimal PayoutRatio { get; set; }

    /// <summary>
    /// 現金殖利率
    /// </summary>
    public decimal CashDividendYield { get; set; }

    /// <summary>
    /// EPS
    /// </summary>
    public decimal EarningsPerShare { get; set; }
}