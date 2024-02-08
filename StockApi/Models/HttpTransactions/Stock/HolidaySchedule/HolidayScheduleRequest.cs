namespace StockApi.Models.HttpTransactions.Stock.HolidaySchedule;

/// <summary>
/// 表示用於獲取股票市場休市日資訊的請求類別。
/// </summary>
/// <param name="Year"></param>
public record HolidayScheduleRequest(int Year) : IRequest
{
    /// <summary>
    /// 指定年度
    /// </summary>
    public int Year { get; } = Year;

    /// <inheritdoc />
    public string KeyWithPrefix()
    {
        return $"{nameof(HolidayScheduleRequest)}:{Year}";
    }
}