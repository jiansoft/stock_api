using StockApi.Models.Entities;

namespace StockApi.Models.HttpTransactions.Stock.Dividend;

/// <summary>
/// 用於表示股利相關數據的 DTO (Data Transfer Object) 類別。
/// </summary>
/// <param name="de"></param>
public class DividendDto(DividendEntity de)
{
    /// <summary>
    /// 除息日
    /// </summary>
    public string ExDividendDate1 { get; set; } = de.ExDividendDate1;

    /// <summary>
    /// 除權日
    /// </summary>
    public string ExDividendDate2 { get; set; } = de.ExDividendDate2;

    /// <summary>
    /// 現金股利發放日
    /// </summary>
    public string PayableDate1 { get; set; } = de.PayableDate1;

    /// <summary>
    /// 股票股利發放日
    /// </summary>
    public string PayableDate2 { get; set; } = de.PayableDate2;

    /// <summary>
    /// 股票代號
    /// </summary>
    public string StockSymbol { get; set; } = de.StockSymbol;

    /// <summary>
    /// 股利所屬季度 空字串:全年度 Q1~Q4:第一季~第四季 H1~H2︰上半季~下半季
    /// </summary>
    public string Quarter { get; set; } = de.Quarter;

    /// <summary>
    /// 股利發放年度
    /// </summary>
    public long Year { get; set; } = de.Year;

    /// <summary>
    /// 股利所屬年度
    /// </summary>
    public long YearOfDividend { get; set; } = de.YearOfDividend;

    /// <summary>
    /// 股利總計
    /// </summary>
    public decimal Sum { get; set; } = de.Sum;

    /// <summary>
    /// 股票股利
    /// </summary>
    public decimal StockDividend { get; set; } = de.StockDividend;

    /// <summary>
    /// 現金股利
    /// </summary>
    public decimal CashDividend { get; set; } = de.CashDividend;

    /// <summary>
    /// 盈餘分配率(%)
    /// </summary>
    public decimal PayoutRatio { get; set; } = de.PayoutRatio;

    /// <summary>
    /// 現金殖利率
    /// </summary>
    public decimal CashDividendYield { get; set; } = Math.Round(de.CashDividendYield, 2);

    /// <summary>
    /// EPS
    /// </summary>
    public decimal EarningsPerShare { get; set; } = de.EarningsPerShare;
}