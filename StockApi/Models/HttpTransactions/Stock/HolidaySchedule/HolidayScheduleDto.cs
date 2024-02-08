namespace StockApi.Models.HttpTransactions.Stock.HolidaySchedule;

/// <summary>
/// 市場休市日
/// </summary>
public class HolidayScheduleDto
{
    /// <summary>
    /// 休市日期
    /// </summary>
    public string Date { get; set; } = string.Empty;
    
    /// <summary>
    /// 休市原因
    /// </summary>
    public string Why { get; set; }= string.Empty;
}